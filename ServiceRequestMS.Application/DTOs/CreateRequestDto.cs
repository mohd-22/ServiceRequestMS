namespace ServiceRequestMS.Application.DTOs;
public class CreateRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryItemId { get; set; }    
}