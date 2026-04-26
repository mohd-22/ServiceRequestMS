using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services;
using ServiceRequestMS.Application.Services.Interfaces;

namespace ServiceRequestMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {

        readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }
        [Authorize] 
        [HttpPost("CreateRequest")]
        public async Task<ActionResult> CreateRequest(CreateRequestDto dto)
        {
            var result = await _requestService.CreateRequest(dto);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("AdminReq")]
        public async Task<ActionResult> GetRequestForAdmin([FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
        {
            var result = await _requestService.GetRequestsForAdminAsync(sortBy, sortOrder);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("EmpReq/{Id}")]
        public async Task<ActionResult> GetRequestForEmployee(Guid Id, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
        {
            var result = await _requestService.GetRequestsForEmployeeAsync(Id, sortBy, sortOrder);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("StaffReq/{Id}")]
        public async Task<ActionResult> GetRequestForStaff(Guid Id, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
        {
            var result = await _requestService.GetRequestsForStaffAsync(Id, sortBy, sortOrder);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteRequest(Guid Id)
        {
            var result = await _requestService.DeleteRequest(Id);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateRequest(UpdateEmployeeRequestDto dto)
        {
            var result = await _requestService.UpdateRequest(dto);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult> GetPagedRequest(int page, int pageSize, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
        {
            var result = await _requestService.GetPagedRequests(page, pageSize, sortBy, sortOrder);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }

    }
}
