using BookApp.Context;
using BookApp.Repository.Interfaces;

namespace BookApp.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookAppContext _context;
        private bool _disposed = false;
        public IRoleRepository Roles { get; }
        public IUserRepository Users { get; }
        public ICategoryRepository Categories { get; }
        public IBookRepository Books { get; }
        public ICommentRepository Comments { get; }
        public IFlagRepository Flags { get; }
        public IBookReportRepository BookReports { get; }
        public ICommentReportRepository CommentReports { get; }

        public UnitOfWork(
            BookAppContext context,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IBookRepository bookRepository,
            ICommentRepository commentRepository,
            IFlagRepository flagRepository,
            IBookReportRepository bookReportRepository,
            ICommentReportRepository commentReportRepository)
        {
            _context = context;
            Roles = roleRepository;
            Users = userRepository;
            Categories = categoryRepository;
            Books = bookRepository;
            Comments = commentRepository;
            Flags = flagRepository;
            BookReports = bookReportRepository;
            CommentReports = commentReportRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
