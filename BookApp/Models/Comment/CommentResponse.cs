﻿namespace BookApp.Models.Comment;

public class CommentResponse
{
    public class CommentResponseModel : BaseResponseModel
    {
        public CommentViewModel Data { get; set; }
    }

    public class CommentsResponseModel : BaseResponseModel
    {
        public List<CommentViewModel> Data { get; set; }
    }
}
