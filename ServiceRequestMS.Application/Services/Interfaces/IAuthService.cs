using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.core.Models;

namespace ServiceRequestMS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto request);
    }
}
