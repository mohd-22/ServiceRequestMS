using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
namespace ServiceRequestMS.Application.Services.Interfaces
{
    public interface ICommentService
    {   
        Task<ApiResponse<CreateCommentDto>> CreateComment(CreateCommentDto commentDto, Guid Userid);
        Task<ApiResponse<IEnumerable<CommentReadDto>>> GetAllComments(Guid requestId);
        Task<ApiResponse<bool>> DeleteComment(Guid commentId);
    }
}
