
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Core.Models;

namespace ServiceRequestMS.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IGenericRepository<Attachment> Attachments { get; }
        ICommentRepository Comments { get; }
        IGenericRepository<Item> Items { get; }
        ICategoryRepository Categories { get; }
        IRequestRepository Requests { get; }


        Task<int> CompleteAsync();
    }
}