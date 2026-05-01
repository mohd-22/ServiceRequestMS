using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IUserService
{    
    Task<ApiResponse<UserRegistraionDto>> CreateUserAsync(UserRegistraionDto request);
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<UserDto>>> GetPagedUsers(int pageNumber, int pageSize, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");
    Task<ApiResponse<bool>> ActivateUserAsync(Guid id);
    Task<ApiResponse<bool>> DeactivateUserAsync(Guid id);
}
