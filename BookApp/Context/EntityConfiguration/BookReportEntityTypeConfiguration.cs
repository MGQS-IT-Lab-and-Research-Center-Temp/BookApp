using BookApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookApp.Context
{
    public class BookReportEntityTypeConfiguration : IEntityTypeConfiguration<BookReport>
    {
        public void Configure(EntityTypeBuilder<BookReport> builder)
        {
            builder.ToTable("BookReport");

            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.AdditionalComment)
                   .HasMaxLength(200);
                             
            builder.HasOne(qr => qr.Book)
                   .WithMany(q => q.BookReports)
                   .HasForeignKey(qr =>  qr.BookId)
                   .IsRequired();

            builder.HasOne(qr => qr.User)
                    .WithMany(u => u.BookReports)
                    .HasForeignKey(qr => qr.UserId)
                    .IsRequired();

            builder.HasMany(c => c.BookReportFlags)
                   .WithOne(cr => cr.BookReport)
                   .IsRequired();
        }
    }
}
