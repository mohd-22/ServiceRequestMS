using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.core.Models;
public class Item : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<Request>? Requests { get; set; }
}
