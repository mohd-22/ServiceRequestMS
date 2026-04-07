using ServiceRequestMS.core.Models;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;

namespace ServiceRequestMS.Data.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

}
