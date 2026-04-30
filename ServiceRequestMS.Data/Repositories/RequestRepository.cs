using Microsoft.EntityFrameworkCore;
using ServiceRequestMS.Core.Models;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;
namespace ServiceRequestMS.Data.Repositories
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Request>> GetPagedRequests(int pageNumber, int pageSize, string? searchTerm = null,string? sortBy = null, string sortOrder = "desc")
        {
            var query = _context.Requests
                .Include(r => r.Requester)
                .Include(r => r.AssignedStaff)
                .Include(r => r.CategoryItem)
                    .ThenInclude(ci => ci!.Category)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();

                query = query.Where(r =>
                    r.Title.ToLower().Contains(searchTerm) ||
                    r.Requester.FullName.ToLower().Contains(searchTerm) ||
                    r.AssignedStaff!.FullName.ToLower().Contains(searchTerm)
                );
            }

            query = ApplySort(query, sortBy, sortOrder);

            return await query
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetAllWithDetailsAsync(string? searchTerm = null,string? sortBy = null, string sortOrder = "desc")
        {
            var query = _context.Requests
                .Include(r => r.Requester)
                .Include(r => r.AssignedStaff)
                .Include(r => r.CategoryItem)
                    .ThenInclude(ci => ci!.Category)
                .AsNoTracking();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();

                query = query.Where(r =>
                    r.Title.ToLower().Contains(searchTerm) ||
                    r.Requester.FullName.ToLower().Contains(searchTerm) ||
                    r.AssignedStaff!.FullName.ToLower().Contains(searchTerm)
                );
            }

            query = ApplySort(query, sortBy, sortOrder);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsByEmpIdAsync(Guid userId,string? searchTerm = null, string? sortBy = null, string sortOrder = "desc")
        {
            var query = _context.Requests
                .Include(r => r.CategoryItem)
                    .ThenInclude(ci => ci!.Category)
                .Include(r => r.AssignedStaff)
                .Where(r => r.CreatedBy == userId)
                .AsNoTracking();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();

                query = query.Where(r =>
                    r.Title.ToLower().Contains(searchTerm) ||
                    r.Requester.FullName.ToLower().Contains(searchTerm) ||
                    r.AssignedStaff!.FullName.ToLower().Contains(searchTerm)
                );
            }

            query = ApplySort(query, sortBy, sortOrder);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsByStaffIdAsync(Guid userId,string? searchTerm = null, string? sortBy = null, string sortOrder = "desc")
        {
            var query = _context.Requests
                    .Include(r => r.Requester)
                    .Include(r => r.CategoryItem)
                        .ThenInclude(ci => ci!.Category)
                    .Where(r => r.AssignedStaffId == userId)
                    .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();

                query = query.Where(r =>
                    r.Title.ToLower().Contains(searchTerm) ||
                    r.Requester.FullName.ToLower().Contains(searchTerm) ||
                    r.AssignedStaff!.FullName.ToLower().Contains(searchTerm)
                );
            }

            query = ApplySort(query, sortBy, sortOrder);
            return await query.ToListAsync();
        }

        private IQueryable<Request> ApplySort(IQueryable<Request> query, string? sortBy, string sortOrder)
        {
            var ascending = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase);
            sortBy = sortBy?.Trim().ToLowerInvariant();

            return sortBy switch
            {
                "title" => ascending ? query.OrderBy(r => r.Title) : query.OrderByDescending(r => r.Title),
                "status" => ascending ? query.OrderBy(r => r.Status) : query.OrderByDescending(r => r.Status),
                "requester" => ascending ? query.OrderBy(r => r.Requester!.FullName) : query.OrderByDescending(r => r.Requester!.FullName),
                "assignedstaff" => ascending ? query.OrderBy(r => r.AssignedStaff!.FullName) : query.OrderByDescending(r => r.AssignedStaff!.FullName),
                "category" => ascending ? query.OrderBy(r => r.CategoryItem!.Category!.Name) : query.OrderByDescending(r => r.CategoryItem!.Category!.Name),
                "item" => ascending ? query.OrderBy(r => r.CategoryItem!.Name) : query.OrderByDescending(r => r.CategoryItem!.Name),
                _ => ascending ? query.OrderBy(r => r.CreatedDate) : query.OrderByDescending(r => r.CreatedDate),
            };
        }
    }
}
