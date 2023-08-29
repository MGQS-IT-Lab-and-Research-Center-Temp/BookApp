using BookApp.Models;
using BookApp.Models.Comment;
using static BookApp.Models.Comment.CommentResponse;

namespace BookApp.Service.Interface;

public interface ICommentService
{
    Task<BaseResponseModel> CreateComment(CreateCommentViewModel request);
    Task<BaseResponseModel> DeleteComment(string commentId);
    Task<BaseResponseModel> UpdateComment(string commentId, UpdateCommentViewModel request);
    Task<CommentResponseModel> GetComment(string commentId);
    Task<CommentsResponseModel> GetAllComment();
}
