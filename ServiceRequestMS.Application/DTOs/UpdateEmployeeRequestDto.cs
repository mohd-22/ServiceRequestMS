namespace ServiceRequestMS.Application.DTOs;
public class UpdateEmployeeRequestDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryItemId { get; set; }
}
