namespace BookApp.Models.BookReport;

public class BookReportResponseModel : BaseResponseModel
{
    public BookReportViewModel Data { get; set; }
}

public class BookReportsResponseModel : BaseResponseModel
{
    public List<BookReportViewModel> Data { get; set; }
}
