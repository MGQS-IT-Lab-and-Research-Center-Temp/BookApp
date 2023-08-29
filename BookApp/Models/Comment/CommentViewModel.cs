using BookApp.Models.CommentReport;

namespace BookApp.Models.Comment;

public class CommentViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string BookId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }
    public List<CommentReportViewModel> CommentReports = new();
}
