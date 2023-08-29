namespace BookApp.Models.Book;

public class BooksResponseModel : BaseResponseModel
{
    public List<BookViewModel> Data { get; set; }
}

public class BookResponseModel : BaseResponseModel
{
    public BookViewModel Data { get; set; }
}