namespace ServiceRequestMS.Application.DTOs;
public class AttachmentDto
{
    public Guid Id { get;  set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}
