using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;

namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IUserService
{
    //Create
    Task<ApiResponse<UserRegistraionDto>> CreateUserAsync(UserRegistraionDto request);
    //Read
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<UserDto>>> GetPagedUsers(int pageNumber, int pageSize, string? sortBy = null, string sortOrder = "desc");

    //Soft Delete
    Task<ApiResponse<bool>> ActivateUserAsync(Guid id);
    Task<ApiResponse<bool>> DeactivateUserAsync(Guid id);
 

}
