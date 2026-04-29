using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IRequestService
{
    //Create
    Task<ApiResponse<RequestDto>> CreateRequest(CreateRequestDto ReqDto);

    //Read 
    Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetRequestsForAdminAsync(string? searchTerm = null,string ? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<IEnumerable<RequestForEmployeeDto>>> GetRequestsForEmployeeAsync(Guid Id, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<IEnumerable<RequestForStaffDto>>> GetRequestsForStaffAsync(Guid id, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");

    Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetPagedRequests(int pageNumber, int pageSize, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");

    //Delete if Request is new
    Task<ApiResponse<bool>> DeleteRequest(Guid Id);
    Task<ApiResponse<bool>> UpdateRequest(UpdateEmployeeRequestDto dto);
}
