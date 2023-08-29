using BookApp.Models;
using BookApp.Models.Book;

namespace BookApp.Service.Interface
{
    public interface IBookService
    {
        Task<BaseResponseModel> Create(CreateBookViewModel createBookDto);
        Task<BaseResponseModel> Delete(string bookId);
        Task<BaseResponseModel> Update(string bookId, UpdateBookViewModel updatebookDto);
        Task<BookResponseModel> GetBook(string bookId);
        Task<BooksResponseModel> GetAllBook();
        Task<BooksResponseModel> GetBookByCategoryId(string categoryId);
        Task<BooksResponseModel> DisplayBook();
    }
}