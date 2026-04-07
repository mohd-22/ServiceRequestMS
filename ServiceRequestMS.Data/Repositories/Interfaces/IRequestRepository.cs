using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.Data.Repositories.Interfaces;
public interface IRequestRepository : IGenericRepository<Request>
{
    Task<IEnumerable<Request>> GetAllWithDetailsAsync();
    Task<IEnumerable<Request>> GetRequestsByEmpIdAsync(Guid userId);
    Task<IEnumerable<Request>> GetRequestsByStaffIdAsync(Guid userId);
}
