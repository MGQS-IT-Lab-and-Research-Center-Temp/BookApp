using BookApp.Entities;
using BookApp.Models;
using BookApp.Models.Comment;
using BookApp.Models.Book;
using BookApp.Models.BookReport;
using BookApp.Repository.Interfaces;
using BookApp.Service.Interface;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BookApp.Service.Implementations;

public class BookService : IBookService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseModel> Create(CreateBookViewModel request)
    {
        var response = new BaseResponseModel();
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _unitOfWork.Users.GetAsync(userIdClaim);

        var book = new Book
        {
            UserId = user.Id,
            BookText = request.BookText,
            ImageUrl = request.ImageUrl
        };

        var categories = await _unitOfWork.Categories.GetAllByIdsAsync(request.CategoryIds);

        var categoryBooks = new HashSet<CategoryBook>();

        foreach (var category in categories)
        {
            var categoryBook = new CategoryBook
            {
                CategoryId = category.Id,
                BookId = book.Id,
                Category = category,
                Book = book
            };

            categoryBooks.Add(categoryBook);
        }

        book.CategoryBooks = categoryBooks;

        try
        {
            await _unitOfWork.Books.CreateAsync(book);
            await _unitOfWork.SaveChangesAsync();
            response.Message = "Book created successfully!";
            response.Status = true;

            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"Failed to create book: {ex.Message}";
            return response;
        }
    }

    public async Task<BaseResponseModel> Update(string bookId, UpdateBookViewModel request)
    {
        var response = new BaseResponseModel();
        var bookExist = await _unitOfWork.Books.ExistsAsync(c => c.Id == bookId);
        var hasComment = await _unitOfWork.Comments.ExistsAsync(c => c.Id == bookId);
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _unitOfWork.Users.GetAsync(userIdClaim);

        if (!bookExist)
        {
            response.Message = "Book does not exist!";
            return response;
        }

        if (hasComment is true)
        {
            response.Message = $"Could not update the Book";
            return response;
        }

        var book = await _unitOfWork.Books.GetAsync(bookId);

        if (book.UserId != user.Id)
        {
            response.Message = "You cannot update this question";
            return response;
        }

        book.BookText = request.BookText;

        try
        {
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();
            response.Message = "Book updated successfully!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"Could not update the Book: {ex.Message}";
            return response;
        }
    }

    public async Task<BaseResponseModel> Delete(string bookId)
    {
        var response = new BaseResponseModel();

        var bookExist = await _unitOfWork.Books.ExistsAsync(q => (q.Id == bookId)
                                    && (q.Id == bookId
                                    && q.IsDeleted == false
                                    && q.IsClosed == false));

        var hasComment = await _unitOfWork.Comments.ExistsAsync(c => c.Id == bookId);

        if (!bookExist)
        {
            response.Message = "Book does not exist!";
            return response;
        }

        if (hasComment is true)
        {
            response.Message = $"Could not delete the Book";
            return response;
        }

        var book = await _unitOfWork.Books.GetAsync(bookId);
        book.IsDeleted = true;

        try
        {
            await _unitOfWork.Books.RemoveAsync(book);
            await _unitOfWork.SaveChangesAsync();
            response.Message = "Book deleted successfully!";
            response.Status = true;

            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"Book delete failed: {ex.Message}";
            return response;
        }
    }

    public async Task<BooksResponseModel> GetAllBook()
    {
        var response = new BooksResponseModel();

        try
        {
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Expression<Func<Book, bool>> expression = q => q.UserId == userIdClaim;

            var books = IsInRole ? await _unitOfWork.Books.GetBooks() : await _unitOfWork.Books.GetBooks(expression);

            if (books.Count == 0)
            {
                response.Message = "No book found!";
                return response;
            }

            response.Data = books
                .Where(q => q.IsDeleted == false)
                .Select(book => new BookViewModel
                {
                    Id = book.Id,
                    BookText = book.BookText,
                    UserName = book.User.UserName,
                    ImageUrl = book.ImageUrl,
                    Comments = book.Comments
                    .Select(comment => new CommentViewModel
                    {
                        Id = comment.Id,
                        CommentText = comment.CommentText,
                        UserName = comment.User.UserName,
                    }).ToList(),
                    BookReports = book.BookReports
                    .Select(report => new BookReportViewModel
                    {
                        Id = report.Id
                    }).ToList()
                }).ToList();

            response.Status = true;
            response.Message = "Success";
        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.StackTrace}";
            return response;
        }

        return response;
    }

    public async Task<BookResponseModel> GetBook(string id)
    {
        var response = new BookResponseModel();
        var bookExist = await _unitOfWork.Books.ExistsAsync(q => q.Id == id && q.IsDeleted == false);
        var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!bookExist)
        {
            response.Message = $"Book with id {id} does not exist!";
            return response;
        }

        var book = IsInRole ? await _unitOfWork.Books.GetBook(q => q.Id == id && !q.IsDeleted) : await _unitOfWork.Books.GetBook(q => q.Id == id
                                            && q.UserId == userIdClaim
                                            && !q.IsDeleted);

        if (book is null)
        {
            response.Message = "Book not found!";
            return response;
        }

        response.Message = "Success";
        response.Status = true;
        response.Data = new BookViewModel
        {
            Id = book.Id,
            BookText = book.BookText,
            UserId = book.UserId,
            UserName = book.User.UserName,
            ImageUrl = book.ImageUrl,
            Comments = book.Comments
                        .Where(c => !c.IsDeleted)
                        .Select(c => new CommentViewModel
                        {
                            Id = c.Id,
                            UserId = c.UserId,
                            CommentText = c.CommentText,
                            UserName = c.User.UserName
                        }).ToList(),
            BookReports = book.BookReports
                              .Where(qr => !qr.IsDeleted)
                              .Select(qr => new BookReportViewModel
                              {
                                  Id = qr.Id,
                                  BookReporter = qr.User.UserName,
                                  AdditionalComment = qr.AdditionalComment
                              }).ToList()
        };

        return response;
    }

    public async Task<BooksResponseModel> GetBookByCategoryId(string categoryId)
    {
        var response = new BooksResponseModel();

        try
        {
            var books = await _unitOfWork.Books.GetBookByCategoryId(categoryId);

            if (books.Count == 0)
            {
                response.Message = "No book found!";
                return response;
            }

            response.Data = books.Select(book => new BookViewModel
            {
                Id = book.Id,
                BookText = book.Book.BookText,
                UserName = book.Book.User.UserName,
                ImageUrl = book.Book.ImageUrl,
            }).ToList();

            response.Status = true;
            response.Message = "Success";
        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.StackTrace}";
            return response;
        }

        return response;
    }

    public async Task<BooksResponseModel> DisplayBook()
    {
        var response = new BooksResponseModel();

        try
        {
            var books = await _unitOfWork.Books.GetBooks();

            if (books.Count == 0)
            {
                response.Message = "No book found!";
                return response;
            }

            response.Data = books
                .Where(q => !q.IsDeleted)
                .Select(book => new BookViewModel
                {
                    Id = book.Id,
                    UserId = book.UserId,
                    BookText = book.BookText,
                    UserName = book.User.UserName,
                    ImageUrl = book.ImageUrl,
                    Comments = book.Comments
                        .Where(c => !c.IsDeleted)
                        .Select(c => new CommentViewModel
                        {
                            Id = c.Id,
                            UserId = c.UserId,
                            CommentText = c.CommentText,
                            UserName = c.User.UserName
                        })
                        .ToList()
                }).ToList();

            response.Status = true;
            response.Message = "Success";
        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.Message}";
            return response;
        }

        return response;
    }
}