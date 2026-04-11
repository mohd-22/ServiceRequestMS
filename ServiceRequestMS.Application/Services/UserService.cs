using Microsoft.AspNetCore.Identity;
using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;
using ServiceRequestMS.Data.Repositories.Interfaces;
using System.Collections.Generic;


namespace ServiceRequestMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<ApiResponse<bool>> ActivateUserAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return ApiResponse<bool>.FailureResponse("User Not found");
            if(user.IsActive == true) { return ApiResponse<bool>.FailureResponse("User is already Active"); }

            user.IsActive = true;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return ApiResponse<bool>.SuccessResponse(true,"user activated Succesfully");
        }

        public async Task<ApiResponse<bool>> DeactivateUserAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return ApiResponse<bool>.FailureResponse("User Not found");
            if (user.IsActive == false) { return ApiResponse<bool>.FailureResponse("User is already Deactive"); }


            bool canDeactivate = true;

            if (user.Role == UserRoles.Staff)
            {
                canDeactivate = await HandleStaffDeactivation(id);
            }
            else if (user.Role == UserRoles.Employee)
            {
                canDeactivate = await HandleEmployeeDeactivation(id);
            }

            if (!canDeactivate) return ApiResponse<bool>.FailureResponse("The employee has incomplete requests yet.");

           
            user.IsActive = false;
            _unitOfWork.Users.Update(user);
             await _unitOfWork.CompleteAsync();
            return ApiResponse<bool>.SuccessResponse(true, "user deactivated Succesfully");

        }


        private async Task<bool> HandleStaffDeactivation(Guid staffId)
        {
            var requests = await _unitOfWork.Requests.FindAllAsync(r =>

                r.AssignedStaffId == staffId &&
                r.Status != RequestStatus.Accepted &&
                r.Status != RequestStatus.Rejected);

            foreach (var req in requests)
            {
                req.Status = RequestStatus.Paused;
                req.AssignedStaffId = null;
                _unitOfWork.Requests.Update(req);
            }
            return true;
        }

      
        private async Task<bool> HandleEmployeeDeactivation(Guid employeeId)
        {
           
            bool hasActiveRequests = await _unitOfWork.Requests.AnyAsync(r => r.CreatedBy == employeeId);
            return !hasActiveRequests;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync() 
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id.ToString(),
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),

                IsActive = user.IsActive
            });

            return ApiResponse<IEnumerable<UserDto>>.SuccessResponse(userDtos,"Users Retrieved Succesfully");
        }

        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id)
        {
          
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return ApiResponse<UserDto>.FailureResponse("User not found");

            var requests = await _unitOfWork.Requests.FindAsNoTrackingAsync(r =>
              r.CreatedBy == id || r.AssignedStaffId == id);

            var userDto = new UserDto
            {
                Id = id.ToString(),
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                UserRequests = requests.Select(req => new RequestDto
                {
                    Id = req.Id,
                    Title = req.Title,
                    Status = req.Status
                }),
                IsActive = user.IsActive
            };

            return ApiResponse<UserDto>.SuccessResponse(userDto, "User Retrieved Succesfully");
        }

        public async Task<ApiResponse<UserRegistraionDto>> CreateUserAsync(UserRegistraionDto request)
        {
            if (await _unitOfWork.Users.AnyAsync(u => u.UserName == request.UserName))
            {
                return ApiResponse<UserRegistraionDto>.FailureResponse("UserName Already Exists");
            }

            var user = new User();
            var hashedPassword = new PasswordHasher<User>().
            HashPassword(user, request.Password);
            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;
            user.Email = request.Email;
            user.Role = request.Role;
            user.PhoneNumber = request.PhoneNumber;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return ApiResponse<UserRegistraionDto>.SuccessResponse(request,"User Created succesfully");
        }

       
    }
}
