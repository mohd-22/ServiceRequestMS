using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ServiceRequestMS.core.Models.Enums;


namespace ServiceRequestMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
        
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(LoginDto request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Wrong username or password");
            }
            return Ok(token);
        }
    }
}
