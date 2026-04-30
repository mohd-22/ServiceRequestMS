using ServiceRequestMS.core.Models.Enums;
namespace ServiceRequestMS.Application.DTOs;
public class UserRegistraionDto
{       
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRoles Role { get; set; }
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
