using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Data.Repositories.Interfaces;
namespace ServiceRequestMS.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<ApiResponse<CreateCommentDto>> CreateComment(CreateCommentDto commentDto, Guid Userid)
        {
            var comment = new Comment
            {
                CommentText = commentDto.Text,
                RequestId = commentDto.RequestId,
                UserId = Userid,
            };
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            return ApiResponse<CreateCommentDto>.SuccessResponse(commentDto, "Comment Created Successfully");
        }
        public async Task<ApiResponse<IEnumerable<CommentReadDto>>> GetAllComments(Guid requestId)
        {
            var comments = await _unitOfWork.Comments.GetAllCommnets(requestId);
            var commentDtos = comments.Select(c => new CommentReadDto
            {
                Id = c.Id,
                Text = c.CommentText,
                CreatedAt = c.CreatedDate,
                UserName = c.User!.FullName, 
                UserRole = c.User.Role.ToString(),
                UserId = c.UserId
            }).ToList();

            return ApiResponse<IEnumerable<CommentReadDto>>.SuccessResponse(commentDtos, "Comments Retrieved Successfully");
        }
        public async Task<ApiResponse<bool>> DeleteComment(Guid commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (comment == null) { return ApiResponse<bool>.FailureResponse("Comment not found!"); }
            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
            return ApiResponse<bool>.SuccessResponse(true, "Comment Deleted Successfully");
        }



    }
}
