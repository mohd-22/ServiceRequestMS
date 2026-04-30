using ServiceRequestMS.core.Models;
namespace ServiceRequestMS.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetPagedUsers(int pageNumber, int pageSize, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc");
    }
}
