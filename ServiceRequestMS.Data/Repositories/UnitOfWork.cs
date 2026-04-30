
using ServiceRequestMS.Data.Repositories.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.data.Data;

namespace ServiceRequestMS.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IGenericRepository<Attachment> Attachments { get; }
        public ICommentRepository Comments { get; }
        public IGenericRepository<Item> Items { get; }
        public ICategoryRepository Categories { get; }
        public IRequestRepository Requests { get; } 
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Attachments = new GenericRepository<Attachment>(_context);
            Comments = new CommentRepository(_context);
            Items = new GenericRepository<Item>(_context);
            Categories = new CategoryRepository(_context);
            Requests = new RequestRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}