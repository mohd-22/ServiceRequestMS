//using Azure.Core;
using AutoMapper;
using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models.Enums;
using ServiceRequestMS.Core.Models;
using ServiceRequestMS.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace ServiceRequestMS.Application.Services;
public class RequestService : IRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<CreateRequestDto>> CreateRequest(CreateRequestDto dto)
    {

        var request = new Request
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryItemId = dto.CategoryItemId,
            Status = RequestStatus.New

        };

        await _unitOfWork.Requests.AddAsync(request);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<CreateRequestDto>.SuccessResponse(dto,"Request Created Succesfully");
    }

    public async Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetRequestsForAdminAsync(string? sortBy = null, string sortOrder = "desc")
    {
        var requests = await _unitOfWork.Requests.GetAllWithDetailsAsync(sortBy, sortOrder);

        return ApiResponse<IEnumerable<RequestAdminDto>>.SuccessResponse(_mapper.Map<IEnumerable<RequestAdminDto>>(requests), "Requests Retrieved Succesfully");

    }

    public async Task<ApiResponse<IEnumerable<RequestForEmployeeDto>>> GetRequestsForEmployeeAsync(Guid Id, string? sortBy = null, string sortOrder = "desc")
    {
        var emp = await _unitOfWork.Users.AnyAsync(x => x.Id == Id && x.Role == UserRoles.Employee);
        if(emp == false)
        {
            return ApiResponse<IEnumerable<RequestForEmployeeDto>>.FailureResponse("Employee not found!");
        }
        var requests = await _unitOfWork.Requests.GetRequestsByEmpIdAsync(Id, sortBy, sortOrder);

        if(requests.Any() == false)
        {
            return ApiResponse<IEnumerable<RequestForEmployeeDto>>.SuccessResponse([],"Employee has no Requests");
        }

        return ApiResponse<IEnumerable<RequestForEmployeeDto>>.SuccessResponse(_mapper.Map<IEnumerable<RequestForEmployeeDto>>(requests), "Requests Retrieved Succesfully");


    }

    public async Task<ApiResponse<IEnumerable<RequestForStaffDto>>> GetRequestsForStaffAsync(Guid Id, string? sortBy = null, string sortOrder = "desc")
    {
        var emp = await _unitOfWork.Users.AnyAsync(x => x.Id == Id && x.Role == UserRoles.Staff);
        if (emp == false)
        {
            return ApiResponse<IEnumerable<RequestForStaffDto>>.FailureResponse("Staff not found!");
        }
        var requests = await _unitOfWork.Requests.GetRequestsByStaffIdAsync(Id, sortBy, sortOrder);
        if (requests.Any() == false)
        {
            return ApiResponse<IEnumerable<RequestForStaffDto>>.SuccessResponse([], "Staff has no Requests assigned to him");
        }

        return ApiResponse<IEnumerable<RequestForStaffDto>>.SuccessResponse(_mapper.Map<IEnumerable<RequestForStaffDto>>(requests), "Requests Retrieved Succesfully");

    }
    public async Task<ApiResponse<bool>> DeleteRequest(Guid Id)
    {
        var request = await _unitOfWork.Requests.GetByIdAsync(Id);
        if (request == null) { return ApiResponse<bool>.FailureResponse("Request not Found"); }

        if (request.Status == RequestStatus.New)
        {
            _unitOfWork.Requests.Delete(request);
            await _unitOfWork.CompleteAsync();
            return ApiResponse<bool>.SuccessResponse(true, "Request Deleted Succesfully");

        }
        return ApiResponse<bool>.FailureResponse("Request must be New to be deleted");
    }
    public async Task<ApiResponse<bool>> UpdateRequest(UpdateEmployeeRequestDto dto)
    {
       
        var request = await _unitOfWork.Requests.GetByIdAsync(dto.Id);

        if (request == null) return ApiResponse<bool>.FailureResponse("Request not Found"); 

        if (request.Status != RequestStatus.New)
        {
            return ApiResponse<bool>.FailureResponse("Cannot update request once it's processed.");
        }
        _mapper.Map(dto, request);

        _unitOfWork.Requests.Update(request);
         await _unitOfWork.CompleteAsync() ;
        return ApiResponse<bool>.SuccessResponse(true, "Request Updated Succesfully");
    }

    public async Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetPagedRequests(int pageNumber, int pageSize, string? sortBy = null, string sortOrder = "desc")
    {
        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (pageSize <= 0)
        {
            pageSize = 5;
        }

        if (_unitOfWork.Requests == null)
            return ApiResponse<IEnumerable<RequestAdminDto>>.FailureResponse("System error: Request repository is not initialized.");

      
        var totalRequests = await _unitOfWork.Requests.CountAsync();

        if (totalRequests == 0)
            return ApiResponse<IEnumerable<RequestAdminDto>>.SuccessResponse(Enumerable.Empty<RequestAdminDto>(), "No requests found.");

       
        var totalPages = (int)Math.Ceiling(totalRequests / (double)pageSize);

        if (pageNumber > totalPages)
        {
            return ApiResponse<IEnumerable<RequestAdminDto>>.SuccessResponseForPaages(
                Enumerable.Empty<RequestAdminDto>(),
                pageNumber,
                "No requests found for this page.",
                totalPages
            );
        }

       
        var requests = await _unitOfWork.Requests.GetPagedRequests(pageNumber, pageSize, sortBy, sortOrder);

      
        var mappedRequests = _mapper.Map<IEnumerable<RequestAdminDto>>(requests);

        return ApiResponse<IEnumerable<RequestAdminDto>>.SuccessResponseForPaages(
            mappedRequests,
            pageNumber,
            "Requests Retrieved Successfully",
            totalPages
        );
    }
}
