using Microsoft.EntityFrameworkCore;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;
namespace ServiceRequestMS.Data.Repositories;
public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<Comment>> GetAllCommnets(Guid id)
    {
        return await _context.Set<Comment>()
        .Include(c => c.User).Include(c => c.Request)
        .Where(c => c.RequestId == id).ToListAsync();
    }
}
