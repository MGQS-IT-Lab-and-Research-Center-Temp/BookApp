using BookApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookApp.Context.EntityConfiguration
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(q => q.Id);

            builder.HasOne(q => q.User)
                .WithMany(u => u.Books)
                .HasForeignKey(q => q.UserId)
                .IsRequired();

            builder.Property(q => q.BookText)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(q => q.ImageUrl)
                .HasColumnType("varchar(255)");

            builder.HasMany(q => q.CategoryBooks)
                .WithOne(cq => cq.Book)
                .IsRequired();

            builder.HasMany(q => q.Comments)
                .WithOne(c => c.Book)
                .IsRequired();

            builder.HasMany(q => q.BookReports)
                .WithOne(qr => qr.Book)
                .IsRequired();
        }
    }
}
