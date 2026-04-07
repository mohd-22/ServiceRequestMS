using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.core.Models;
public class Attachment : BaseEntity
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public Guid RequestId { get; set; }
    public Guid UserId { get; set; }
    public Request? Request { get; set; }
    public User? User { get; set; }
}
