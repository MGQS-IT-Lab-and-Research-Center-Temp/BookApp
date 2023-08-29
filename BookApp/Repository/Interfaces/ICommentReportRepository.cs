using BookApp.Entities;

namespace BookApp.Repository.Interfaces
{
    public interface ICommentReportRepository : IRepository<CommentReport>
    {
        Task<List<CommentReport>> GetCommentReports();
        Task<CommentReport> GetCommentReport(string id);
    }
}
