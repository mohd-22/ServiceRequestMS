using ServiceRequestMS.core.Models;
namespace ServiceRequestMS.Data.Repositories.Interfaces;
public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IGenericRepository<Attachment> Attachments { get; }
    ICommentRepository Comments { get; }
    IGenericRepository<Item> Items { get; }
    ICategoryRepository Categories { get; }
    IRequestRepository Requests { get; }

    Task<int> CompleteAsync();
}