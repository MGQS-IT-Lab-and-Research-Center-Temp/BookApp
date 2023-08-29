using BookApp.Context;
using BookApp.Entities;
using BookApp.Repository.Implementations;
using BookApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookApp.Repository.Implementations;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(BookAppContext context) : base(context)
    {
    }

    public async Task<Book> GetBook(Expression<Func<Book, bool>> expression)
    {
        var book = await _context.Books
            .Include(c => c.User)
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .SingleOrDefaultAsync(expression);

        return book;
    }

    public async Task<List<Book>> GetBooks()
    {
        var books = await _context.Books
            .Include(uq => uq.User)
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .Include(qr => qr.BookReports)
            .ToListAsync();

        return books;
    }

    public async Task<List<Book>> GetBooks(Expression<Func<Book, bool>> expression)
    {
        var books = await _context.Books
            .Where(expression)
            .Include(u => u.User)
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .Include(qr => qr.BookReports)
            .ToListAsync();

        return books;
    }

    public async Task<List<CategoryBook>> GetBookByCategoryId(string categoryId)
    {
        var books = await _context.CategoryBooks
            .Include(c => c.Category)
            .Include(c => c.Book)
            .ThenInclude(c => c.User)
            .Where(c => c.CategoryId.Equals(categoryId))
            .ToListAsync();

        return books;
    }

    public async Task<List<CategoryBook>> SelectBookByCategory()
    {
        var books = await _context.CategoryBooks
            .Include(c => c.Category)
            .Include(c => c.Book)
            .ThenInclude(c => c.User)
            .ToListAsync();

        return books;
    }
}
