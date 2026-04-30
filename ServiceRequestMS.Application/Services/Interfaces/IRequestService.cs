using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IRequestService
{
    Task<ApiResponse<RequestDto>> CreateRequest(CreateRequestDto ReqDto); 
    Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetRequestsForAdminAsync(string? searchTerm = null,string ? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<IEnumerable<RequestForEmployeeDto>>> GetRequestsForEmployeeAsync(Guid Id, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<IEnumerable<RequestForStaffDto>>> GetRequestsForStaffAsync(Guid id, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetPagedRequests(int pageNumber, int pageSize, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<bool>> DeleteRequest(Guid Id, Guid currentUserId);
    Task<ApiResponse<bool>> UpdateRequest(UpdateEmployeeRequestDto dto);
}
