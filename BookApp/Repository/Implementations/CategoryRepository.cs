using BookApp.Context;
using BookApp.Entities;
using BookApp.Repository.Interfaces;

namespace BookApp.Repository.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookAppContext context) : base(context)
        { 
        }
    }
}
