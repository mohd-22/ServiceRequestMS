using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Data.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceRequestMS.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService( IConfiguration configuration, IUnitOfWork unitOfWork) { 
          
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto request)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.UserName == request.UserName);

            if (user == null)
                return ApiResponse<string>.FailureResponse("Invalid username or password.");
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return ApiResponse<string>.FailureResponse("Invalid username or password.");
            var Cheakuser = await _unitOfWork.Users.FindAsync(u => u.UserName == request.UserName);

            if (Cheakuser != null && !Cheakuser.IsActive)       
            {
                return ApiResponse<string>.FailureResponse("Your account has been deactivated. Please contact the administrator.");
                // throw new Exception("Your account has been deactivated. Please contact the administrator.");
            }

            return ApiResponse<string>.SuccessResponse(CreateToken(user),"Login successful.");
        }

        private string CreateToken(User user)
        {
            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName),
            };

            var key = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:SecretKey")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var TokenDescirptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
                audience: _configuration.GetValue<string>("JwtSettings:Audience"),
                claims: Claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(TokenDescirptor);
        }

    }
}
