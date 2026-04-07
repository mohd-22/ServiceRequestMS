using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.core.Models;
public class Comment : BaseEntity
{
    public Guid RequestId { get; set; }
    public Guid UserId { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public Request? Request { get; set; }
    public User? User { get; set; }
}
