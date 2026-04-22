using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IRequestService
{
    //Create
    Task<ApiResponse<CreateRequestDto>> CreateRequest(CreateRequestDto ReqDto);

    //Read 
    Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetRequestsForAdminAsync();
    Task<ApiResponse<IEnumerable<RequestForEmployeeDto>>> GetRequestsForEmployeeAsync(Guid Id);
    Task<ApiResponse<IEnumerable<RequestForStaffDto>>> GetRequestsForStaffAsync(Guid id);

    Task<ApiResponse<IEnumerable<RequestAdminDto>>> GetPagedRequests(int pageNumber);

    //Delete if Request is new
    Task<ApiResponse<bool>> DeleteRequest(Guid Id);
    Task<ApiResponse<bool>> UpdateRequest(UpdateEmployeeRequestDto dto);
}
