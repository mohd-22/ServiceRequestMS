using Microsoft.EntityFrameworkCore;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;

namespace ServiceRequestMS.Data.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
    public async Task<Category> GetCategoryWithItemsAsync(Guid id)
    {
        return (await _context.Set<Category>()
        .Include(c => c.Items) 
        .FirstOrDefaultAsync(c => c.Id == id))!;
    }

}
