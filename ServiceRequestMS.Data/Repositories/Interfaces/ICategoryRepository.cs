using ServiceRequestMS.core.Models;
namespace ServiceRequestMS.Data.Repositories.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    public Task<Category> GetCategoryWithItemsAsync(Guid id);
}
