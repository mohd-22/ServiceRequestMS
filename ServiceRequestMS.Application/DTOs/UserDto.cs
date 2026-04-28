
namespace ServiceRequestMS.Application.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public IEnumerable<RequestDto> UserRequests { get; set; } = new List<RequestDto>();
        public DateTime CreatedAt { get; set; } 
    }
}
