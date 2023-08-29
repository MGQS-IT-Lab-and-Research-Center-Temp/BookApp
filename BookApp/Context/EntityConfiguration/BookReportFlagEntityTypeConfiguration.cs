using BookApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookApp.Context.EntityConfiguration
{
    public class BookReportFlagEntityTypeConfiguration : IEntityTypeConfiguration<BookReportFlag>
    {
        public void Configure(EntityTypeBuilder<BookReportFlag> builder)
        {
            builder.ToTable("BookReportFlags");
            builder.Ignore(qrf => qrf.Id);
            builder.HasKey(qr => new { qr.BookReportId, qr.FlagId });

            builder.HasOne(qr => qr.BookReport)
                .WithMany(qrf => qrf.BookReportFlags)
                .HasForeignKey(qr => qr.BookReportId)
                .IsRequired();

            builder.HasOne(qf => qf.Flag)
                .WithMany(qrf => qrf.BookReportFlags)
                .HasForeignKey(qf => qf.FlagId)
                .IsRequired();
        }
    }
}
