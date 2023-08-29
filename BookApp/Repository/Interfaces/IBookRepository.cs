using BookApp.Entities;
using System.Linq.Expressions;

namespace BookApp.Repository.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetBooks();
    Task<List<Book>> GetBooks(Expression<Func<Book, bool>> expression);
    Task<Book> GetBook(Expression<Func<Book, bool>> expression);
    Task<List<CategoryBook>> GetBookByCategoryId(string id);
    Task<List<CategoryBook>> SelectBookByCategory();
}
