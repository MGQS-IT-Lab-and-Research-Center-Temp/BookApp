﻿using System.ComponentModel.DataAnnotations;

namespace BookApp.Models.Comment;

public class CreateCommentViewModel
{
    public string UserId { get; set; }
    public string BookId { get; set; }
    [Required(ErrorMessage = "Comment text cannot be empty")]
    [MinLength(20, ErrorMessage = "The minimum length is 20.")]

    public string CommentText { get; set; }
}
