using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;
using ServiceRequestMS.Services;

namespace ServiceRequestMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
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
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

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
       

    }
}
