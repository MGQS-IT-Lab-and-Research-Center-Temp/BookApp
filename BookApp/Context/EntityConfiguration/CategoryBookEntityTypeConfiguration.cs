using BookApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Context.EntityConfiguration
{
	public class CategoryBookEntityTypeConfiguration : IEntityTypeConfiguration<CategoryBook>
	{
		public void Configure(EntityTypeBuilder<CategoryBook> builder)
		{
			builder.ToTable("CategoryBooks");
            builder.Ignore(cq => cq.Id);
            builder.HasKey(cq => new { cq.CategoryId, cq.BookId });

			builder.HasOne(cq => cq.Category)
				.WithMany(c => c.CategoryBooks)
				.HasForeignKey(cq => cq.CategoryId)
				.IsRequired();

			builder.HasOne(cq => cq.Book)
				.WithMany(q => q.CategoryBooks)
				.HasForeignKey(cq => cq.BookId)
				.IsRequired(); 
		}
	}

}
