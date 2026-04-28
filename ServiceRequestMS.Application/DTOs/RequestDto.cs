using ServiceRequestMS.core.Models.Enums;

namespace ServiceRequestMS.Application.DTOs;

public class RequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
}
