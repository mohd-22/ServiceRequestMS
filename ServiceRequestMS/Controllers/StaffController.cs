using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;

namespace ServiceRequestMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpPost("AssignStaff")]
        public async Task<ActionResult<User>> AsignRequestToStaff(Guid RequestId, Guid StaffId)
        {
            var result = await _staffService.AssignRequestToStaff(RequestId, StaffId);

            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("UpdateStaffRequestStatus")]
        public async Task<ActionResult<User>> UpdateStaffRequestStatus(Guid requestId, RequestStatus nextStatus, string? staffNotes)
        {
            var result = await _staffService.UpdateStaffRequestStatusAsync(requestId, nextStatus,staffNotes);

            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
