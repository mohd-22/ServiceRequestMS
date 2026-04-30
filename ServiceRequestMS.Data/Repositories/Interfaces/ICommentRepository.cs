using ServiceRequestMS.core.Models;
namespace ServiceRequestMS.Data.Repositories.Interfaces;
public interface ICommentRepository : IGenericRepository<Comment>
{
    public Task<IEnumerable<Comment>> GetAllCommnets(Guid id);

}
