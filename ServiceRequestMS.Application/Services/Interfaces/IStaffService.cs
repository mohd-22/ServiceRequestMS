using ServiceRequestMS.Application.Common;
using ServiceRequestMS.core.Models.Enums;
namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IStaffService
{
    Task<ApiResponse<bool>> AssignRequestToStaff(Guid RequestId, Guid StaffId);
    Task<ApiResponse<bool>> UpdateStaffRequestStatusAsync(Guid requestId, RequestStatus nextStatus, string? staffNotes);
}
