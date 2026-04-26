using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.Data.Repositories.Interfaces;
public interface IRequestRepository : IGenericRepository<Request>
{
    Task<IEnumerable<Request>> GetAllWithDetailsAsync(string? sortBy = null, string sortOrder = "desc");
    Task<IEnumerable<Request>> GetRequestsByEmpIdAsync(Guid userId, string? sortBy = null, string sortOrder = "desc");
    Task<IEnumerable<Request>> GetRequestsByStaffIdAsync(Guid userId, string? sortBy = null, string sortOrder = "desc");
    Task<IEnumerable<Request>> GetPagedRequests(int pageNumber, int pageSize, string? sortBy = null, string sortOrder = "desc");
}
