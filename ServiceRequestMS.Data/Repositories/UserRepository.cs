using Microsoft.EntityFrameworkCore;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;

namespace ServiceRequestMS.Data.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<User>> GetPagedUsers(int pageNumber, int pageSize, string? searchTerm = null, string? sortBy = null, string sortOrder = "desc")
    {
        var query = _context.Users.Include(u => u.CreatedRequests).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.Trim().ToLower();
            query = query.Where(u => u.FullName.ToLower().Contains(searchTerm) ||
                                     u.Email.ToLower().Contains(searchTerm) ||
                                     u.PhoneNumber.ToLower().Contains(searchTerm));
        }

        query = ApplySort(query, sortBy, sortOrder);

        return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
    }
    private IQueryable<User> ApplySort(IQueryable<User> query, string? sortBy, string sortOrder)
    {
        var ascending = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase);
        sortBy = sortBy?.Trim().ToLowerInvariant();

        return sortBy switch
        {
            "fullname" => ascending ? query.OrderBy(u => u.FullName) : query.OrderByDescending(u => u.FullName),
            "username" => ascending ? query.OrderBy(r => r.UserName) : query.OrderByDescending(r => r.UserName),
            "email" => ascending ? query.OrderBy(r => r.Email) : query.OrderByDescending(r => r.Email),
            "role" => ascending ? query.OrderBy(r => r.Role) : query.OrderByDescending(r => r.Role),
            "status" => ascending ? query.OrderBy(r => r.IsActive) : query.OrderByDescending(r => r.IsActive),
            "createdat" or "createddate" => ascending ? query.OrderBy(r => r.CreatedDate) : query.OrderByDescending(r => r.CreatedDate),
            _ => ascending ? query.OrderBy(r => r.CreatedDate) : query.OrderByDescending(r => r.CreatedDate),
        };
    }
}
