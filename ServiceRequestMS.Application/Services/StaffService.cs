using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models.Enums;
using ServiceRequestMS.Data.Repositories.Interfaces;

namespace ServiceRequestMS.Application.Services;
public class StaffService : IStaffService
{
    private readonly IUnitOfWork _unitOfWork;

    public StaffService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    public async Task<ApiResponse<bool>> AssignRequestToStaff(Guid RequestId, Guid StaffId)
    {
        var request = await _unitOfWork.Requests.GetByIdAsync(RequestId);
        if (request == null)
        {
            return ApiResponse<bool>.FailureResponse("Request not Found");
        }
        var staff = await _unitOfWork.Users.GetByIdAsync(StaffId);
        if (staff == null)
        {
            return ApiResponse<bool>.FailureResponse("Staff not Found");

        }
        if (!staff.IsActive)
        {
            return ApiResponse<bool>.FailureResponse("Staff Is Deactivated");

        }
        if (request.AssignedStaffId != null)
        {
            return ApiResponse<bool>.FailureResponse("There are already staff assigned to this request");
        }
        if (request.Status == RequestStatus.New || request.Status == RequestStatus.Paused)
        {
            request.AssignedStaffId = staff.Id;
            request.Status = RequestStatus.Assigned;
            await _unitOfWork.CompleteAsync();
            return ApiResponse<bool>.SuccessResponse(true, "Staff assigned to this request succesfully");
        }
        return ApiResponse<bool>.FailureResponse("you cant assign staff to a request unless it 'New' or 'Paused' ");

    }
    public async Task<ApiResponse<bool>> UpdateStaffRequestStatusAsync(Guid requestId, RequestStatus nextStatus, string? staffNotes)
    {
        var request = await _unitOfWork.Requests.GetByIdAsync(requestId);
        if (request == null) return ApiResponse<bool>.FailureResponse("Request not Found");
        ;

        var currentStatus = request.Status;


        if (currentStatus == RequestStatus.Assigned && nextStatus == RequestStatus.InProgress)
        {
            request.Status = RequestStatus.InProgress;
        }
        else if (currentStatus == RequestStatus.InProgress)
        {
            if (nextStatus == RequestStatus.Accepted || nextStatus == RequestStatus.Rejected)
            {
                request.Status = nextStatus;


                if (nextStatus == RequestStatus.Rejected)
                {
                    request.RejectionReason = staffNotes;
                    request.AssignedStaffId = null;
                }
            }
            else
            {
                return ApiResponse<bool>.FailureResponse("Invalid transition. After 'InProgress', you must choose 'Accepted'  or 'Rejected'.");
            }
        }
        else
        {
            return ApiResponse<bool>.FailureResponse($"Cannot update the status of a request that is currently '{currentStatus}'. The process flow has been violated.");
        }

        _unitOfWork.Requests.Update(request);
        await _unitOfWork.CompleteAsync();
        return ApiResponse<bool>.SuccessResponse(true,$"Request Status updated succefully from {currentStatus} to {nextStatus}");
    }
}
