using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;
namespace ServiceRequestMS.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpPost("DeactiveUser/{userId}")]
    public async Task<ActionResult> DeactivateUser(Guid userId)
    {
        var result = await _userService.DeactivateUserAsync(userId);
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpPost("ActiveUser/{userId}")]
    public async Task<ActionResult> ActivateUser(Guid userId)
    {
        var result = await _userService.ActivateUserAsync(userId);
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpGet("GetUserById/{userId}")]
    public async Task<ActionResult> GetUserById(Guid userId)
    {
        var result = await _userService.GetUserByIdAsync(userId);

        if (result.Success == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpPost("AddUser")]
    public async Task<ActionResult<User>> Register(UserRegistraionDto request)
    {

        var result = await _userService.CreateUserAsync(request);

        if (result.Success == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Roles = $"{nameof(UserRoles.Admin)},{nameof(UserRoles.Manager)}")]
    [HttpGet("PagedUser/{page}/{pageSize}")]
    public async Task<ActionResult> GetPagedUsers(int page, int pageSize, string? searchTerm = null, [FromQuery] string? sortBy = null, [FromQuery] string sortOrder = "desc")
    {
        var result = await _userService.GetPagedUsers(page, pageSize, searchTerm ,sortBy, sortOrder);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }
}
