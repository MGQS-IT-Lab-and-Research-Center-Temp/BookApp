using BookApp.Context;
using BookApp.Entities;
using BookApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Repository.Implementations;

public class CommentReportRepository : BaseRepository<CommentReport>, ICommentReportRepository
{
    public CommentReportRepository(BookAppContext context) : base(context)
    {
    }

    public async Task<CommentReport> GetCommentReport(string id)
    {
        var commentReport = await _context.CommentReports
            .Include(u => u.User)
            .Include(c => c.Comment)
            .Include(crf => crf.CommentReportFlags)
            .ThenInclude(f => f.Flag)
            .Where(cr => cr.Id.Equals(id))
            .FirstOrDefaultAsync();

        return commentReport;
    }

    public async Task<List<CommentReport>> GetCommentReports()
    {
        var commentReports = await _context.CommentReports
                .Include(c => c.User)
                .Include(c => c.Comment)
                .Include(c => c.CommentReportFlags)
                .ThenInclude(c => c.Flag)
                .ToListAsync();

        return commentReports;
    }
}
