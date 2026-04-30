using ServiceRequestMS.core.Models.Enums;
using ServiceRequestMS.Core.Models;
namespace ServiceRequestMS.core.Models;
public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty ;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRoles Role { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;    
    public ICollection<Request> CreatedRequests { get; set; } = new List<Request>();
    public ICollection<Request> AssignedRequests { get; set; } = new List<Request>();
}
