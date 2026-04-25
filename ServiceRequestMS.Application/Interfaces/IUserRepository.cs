using ServiceRequestMS.core.Models;
using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
       Task<IEnumerable<User>> GetPagedUsers(int pageNumber, int pageSize);

    }
}
