using BookApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookApp.Context;

public class BookAppContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookAppContext(DbContextOptions<BookAppContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<BookReport> BookReports { get; set; }
    public DbSet<BookReportFlag> BookReportFlags { get; set; }
    public DbSet<CategoryBook> CategoryBooks { get; set; }
    public DbSet<CommentReport> CommentReports { get; set; }
    public DbSet<CommentReportFlag> CommentReportFlags { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatuses();
        this.AddAuditInfo(_httpContextAccessor);
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatuses();
        this.AddAuditInfo(_httpContextAccessor);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private const string IsDeletedProperty = "IsDeleted";

    private void UpdateSoftDeleteStatuses()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.CurrentValues[IsDeletedProperty] = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.CurrentValues[IsDeletedProperty] = true;
                    break;
            }
        }
    }
}
