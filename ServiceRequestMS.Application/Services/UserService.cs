using Microsoft.AspNetCore.Identity;
using ServiceRequestMS.Application.Common;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetPagedUsers(int pageNumber, int pageSize, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc")
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 5;
            }

            if (_unitOfWork.Users == null)
                return ApiResponse<IEnumerable<UserDto>>.FailureResponse("System error: User repository is not initialized.");


            var totalUsers = await _unitOfWork.Users.CountAsync();

            if (totalUsers == 0)
                return ApiResponse<IEnumerable<UserDto>>.SuccessResponse(Enumerable.Empty<UserDto>(), "No Users found.");


            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            if (pageNumber > totalPages)
            {
                return ApiResponse<IEnumerable<UserDto>>.SuccessResponseForPaages(
                    Enumerable.Empty<UserDto>(),
                    pageNumber,
                    "No users found for this page.",
                    totalPages
                );
            }


            var users = await _unitOfWork.Users.GetPagedUsers(pageNumber, pageSize,searchTerm, sortBy, sortOrder);


            var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);

            return ApiResponse<IEnumerable<UserDto>>.SuccessResponseForPaages(
                mappedUsers,
                pageNumber,
                "Users Retrieved Successfully",
                totalPages
            );
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
                IsActive = user.IsActive,
                CreatedAt = user.CreatedDate
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
                IsActive = user.IsActive,
                CreatedAt = user.CreatedDate
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
            user.CreatedDate = DateTime.UtcNow;
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
