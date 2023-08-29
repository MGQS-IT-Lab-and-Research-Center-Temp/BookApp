using BookApp.Repository.Implementations;
using BookApp.Context;
using BookApp.Entities;
using BookApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Repository.Implementations;

public class BookReportRepository : BaseRepository<BookReport> , IBookReportRepository
{
    public BookReportRepository(BookAppContext context) : base(context)
    {
    }

    public async Task<BookReport> GetBookReport(string id)
    {
        var bookReport = await _context.BookReports
            .Include(u => u.User)
            .Include(c => c.Book)
            .Include(crf => crf.BookReportFlags)
            .ThenInclude(f => f.Flag)
            .Where(cr => cr.Id.Equals(id))
            .FirstOrDefaultAsync();

        return bookReport;
    }

    public async Task<List<BookReport>> GetBookReports(string bookId)
    {
        var bookWithReports = await _context.BookReports
                    .Where(qr => qr.BookId.Equals(bookId))
                    .Include(qr => qr.User)
                    .Include(qr => qr.BookReportFlags)
                        .ThenInclude(f => f.Flag)
                    .ToListAsync();

        return bookWithReports;
    }
}
