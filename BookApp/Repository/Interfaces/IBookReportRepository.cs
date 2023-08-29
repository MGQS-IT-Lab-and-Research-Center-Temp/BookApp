using BookApp.Entities;

namespace BookApp.Repository.Interfaces;

public interface IBookReportRepository : IRepository<BookReport>
{
    Task<BookReport> GetBookReport(string reportId);
    Task<List<BookReport>> GetBookReports(string bookId);
}
