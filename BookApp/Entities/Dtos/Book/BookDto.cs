using BookApp.Entities.Dtos.Comment;
using BookApp.Entities.Dtos.QuestionReport;

namespace BookApp.Entities.Dtos.Question;

public class BookDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string BookText { get; set; }
    public string ImageUrl { get; set; }
    public string UserName { get; set; }
    public List<CommentDto> Comments { get; set; }
    public List<BookReportDto> BookReports { get; set; }
}
