using ServiceRequestMS.core.Models;

namespace ServiceRequestMS.Application.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<CategoryItemDto> Items { get; set; } = new List<CategoryItemDto>();
    }
}
