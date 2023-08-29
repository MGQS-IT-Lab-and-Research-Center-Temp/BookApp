using BookApp.Entities.Dtos.CommentReport;

namespace BookApp.Entities.Dtos.Comment;

public class CommentDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string BookId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }
    public List<CommentReportDto> CommentReports = new();
}