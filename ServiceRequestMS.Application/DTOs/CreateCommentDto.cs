namespace ServiceRequestMS.Application.DTOs;
public class CreateCommentDto
{
    public string Text { get; set; } = string.Empty;
    public Guid RequestId { get; set; }
}
