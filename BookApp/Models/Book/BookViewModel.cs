using BookApp.Models.Comment;
using BookApp.Models.BookReport;

namespace BookApp.Models.Book;

public class BookViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string BookText { get; set; }
    public string ImageUrl { get; set; }
    public string UserName { get; set; }
    public List<CommentViewModel> Comments { get; set; }
    public List<BookReportViewModel> BookReports { get; set; }
}
