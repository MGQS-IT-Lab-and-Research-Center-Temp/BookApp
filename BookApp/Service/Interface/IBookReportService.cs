using BookApp.Models;
using BookApp.Models.BookReport;

namespace BookApp.Service.Interface;

public interface IBookReportService
{
    Task<BaseResponseModel> CreateBookReport(CreateBookReportViewModel request);
    Task<BaseResponseModel> DeleteBookReport(string id);
    Task<BaseResponseModel> UpdateBookReport(string id, UpdateBookReportViewModel request);
    Task<BookReportResponseModel> GetBookReport(string reportId);
    Task<BookReportsResponseModel> GetBookReports(string questionId);
}
