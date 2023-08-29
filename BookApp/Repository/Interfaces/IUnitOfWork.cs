namespace BookApp.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    IBookRepository Books { get; }
    ICommentRepository Comments { get; }
    IFlagRepository Flags { get; }
    IBookReportRepository BookReports { get; }
    ICommentReportRepository CommentReports { get; }
    Task<int> SaveChangesAsync();
}