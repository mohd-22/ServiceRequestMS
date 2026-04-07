using Microsoft.EntityFrameworkCore;
using ServiceRequestMS.Core.Models;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;

namespace ServiceRequestMS.Data.Repositories
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Request>> GetAllWithDetailsAsync()
        {
            return await _context.Requests
                .Include(r => r.Requester)
                .Include(r => r.AssignedStaff)
                .Include(r => r.CategoryItem)
                    .ThenInclude(ci => ci.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsByEmpIdAsync(Guid userId)
        {
            return await _context.Requests
                .Include(r => r.CategoryItem)
                    .ThenInclude(ci => ci.Category)
                .Include(r => r.AssignedStaff)
                .Where(r => r.CreatedBy == userId) 
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Request>> GetRequestsByStaffIdAsync(Guid userId)
        {
            return await _context.Requests
                    .Include(r => r.Requester)
                    .Include(r => r.CategoryItem)
                        .ThenInclude(ci => ci.Category)
                    .Where(r => r.AssignedStaffId == userId)
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}
