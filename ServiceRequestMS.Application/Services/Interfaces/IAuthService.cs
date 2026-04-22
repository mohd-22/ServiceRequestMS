using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.core.Models;

namespace ServiceRequestMS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> LoginAsync(LoginDto request);
    }
}
