using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.core.Models;
public class Comment : BaseEntity
{
    public string CommentText { get; set; } = string.Empty;
    public Guid RequestId { get; set; }
    public Request? Request { get; set; }   
    public User? User { get; set; }
    public Guid UserId { get; set; }
}
