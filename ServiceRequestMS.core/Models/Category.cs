using System.Security.Principal;

namespace ServiceRequestMS.core.Models;
public class Category : BaseEntity 
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
