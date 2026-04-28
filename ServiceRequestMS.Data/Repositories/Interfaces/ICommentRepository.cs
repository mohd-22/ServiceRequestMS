using ServiceRequestMS.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestMS.Data.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        public Task<IEnumerable<Comment>> GetAllCommnets(Guid id);

    }
}
