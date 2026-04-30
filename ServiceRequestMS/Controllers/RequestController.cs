using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using System.Security.Claims;

namespace ServiceRequestMS.Api.Controllers;
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
    public async Task<ActionResult> GetRequestForAdmin([FromQuery] string? searchTerm = null,[FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
    {
        var result = await _requestService.GetRequestsForAdminAsync(searchTerm,sortBy, sortOrder);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("EmpReq/{Id}")]
    public async Task<ActionResult> GetRequestForEmployee(Guid Id, [FromQuery] string? searchTerm = null, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
    {
        var result = await _requestService.GetRequestsForEmployeeAsync(Id, searchTerm,sortBy, sortOrder);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("StaffReq/{Id}")]
    public async Task<ActionResult> GetRequestForStaff(Guid Id, [FromQuery] string? searchTerm = null, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
    {
        var result = await _requestService.GetRequestsForStaffAsync(Id, searchTerm, sortBy, sortOrder);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [Authorize(Roles = "Employee")]
    [HttpDelete]
    public async Task<ActionResult> DeleteRequest(Guid Id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var currentUserId))
        {
            return Unauthorized("Unable to identify the current user.");
        }

        var result = await _requestService.DeleteRequest(Id, currentUserId);
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
    public async Task<ActionResult> GetPagedRequest(int page, int pageSize, [FromQuery] string? searchTerm = null, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
    {
        var result = await _requestService.GetPagedRequests(page, pageSize, searchTerm, sortBy, sortOrder);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

}
