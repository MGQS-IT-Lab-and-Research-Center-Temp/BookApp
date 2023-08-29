using BookApp.Entities;
using BookApp.Models;
using BookApp.Models.BookReport;
using BookApp.Repository.Interfaces;
using BookApp.Service.Interface;
using System.Security.Claims;

namespace BookApp.Service.Implementations;

public class BookReportService : IBookReportService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public BookReportService(IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResponseModel> CreateBookReport(CreateBookReportViewModel request)
    {
        var response = new BaseResponseModel();
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var reporter = await _unitOfWork.Users.GetAsync(userIdClaim);
        var book = await _unitOfWork.Books.GetAsync(request.BookId);

        if (reporter is null)
        {
            response.Message = "User not found!";
            return response;
        }

        if (book is null)
        {
            response.Message = "Book not found!";
            return response;
        }

        var bookReport = new BookReport
        {
            UserId = reporter.Id,
            User = reporter,
            BookId = book.Id,
            Book = book,
            AdditionalComment = request.AdditionalComment
        };

        var flags = await _unitOfWork.Flags.GetAllByIdsAsync(request.FlagIds);

        var bookFlags = new HashSet<BookReportFlag>();

        foreach (var flag in flags)
        {
            var bookReportFlag = new BookReportFlag
            {
                FlagId = flag.Id,
                BookReportId = bookReport.Id,
                Flag = flag,
                BookReport = bookReport
            };

            bookFlags.Add(bookReportFlag);
        }
        bookReport.BookReportFlags = bookFlags;
        try
        {
            await _unitOfWork.BookReports.CreateAsync(bookReport);
            response.Status = true;
            response.Message = "Report created successfully!";
            await _unitOfWork.SaveChangesAsync();
            return response;

        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.StackTrace}";
            return response;
        }

    }

    public async Task<BaseResponseModel> DeleteBookReport(string id)
    {
        var response = new BaseResponseModel();

        var isBookReportExist = await _unitOfWork.BookReports.ExistsAsync(c => c.Id == id);

        if (!isBookReportExist)
        {
            response.Message = "Report does not exist!";
            return response;
        }

        var bookReport = await _unitOfWork.BookReports.GetAsync(id);

        try
        {
            await _unitOfWork.BookReports.RemoveAsync(bookReport);
        }
        catch (Exception ex)
        {
            response.Message = $"Question report delete failed: {ex.Message}";
            return response;
        }

        response.Status = true;
        response.Message = "Book report deleted successfully!";
        await _unitOfWork.SaveChangesAsync();
        return response;
    }

    public async Task<BookReportResponseModel> GetBookReport(string id)
    {
        var response = new BookReportResponseModel();

        var isBookReportExist = await _unitOfWork.BookReports.ExistsAsync(c => c.Id == id);

        if (!isBookReportExist)
        {
            response.Message = $"Report with id {id} does not exist!";
            return response;
        }

        var bookReport = await _unitOfWork.BookReports.GetBookReport(id);

        response.Message = "Success";
        response.Status = true;

        response.Data = new BookReportViewModel
        {
            Id = id,
            AdditionalComment = bookReport.AdditionalComment,
            BookId = bookReport.Book.Id,
            BookReporter = bookReport.User.UserName,
            BookText = bookReport.Book.BookText,
            FlagNames = bookReport.BookReportFlags
                                .Select(f => f.Flag.FlagName)
                                .ToList(),
        };

        return response;
    }

    public async Task<BookReportsResponseModel> GetBookReports(string id)
    {
        var response = new BookReportsResponseModel();

        try
        {
            var bookWithReports = await _unitOfWork.BookReports.GetBookReports(id);

            response.Data = bookWithReports
                .Select(qr => new BookReportViewModel
                {
                    Id = qr.Id,
                    BookId = qr.BookId,
                    BookReporter = qr.User.UserName,
                    AdditionalComment = qr.AdditionalComment,
                    FlagNames = qr.BookReportFlags
                                    .Select(f => f.Flag.FlagName)
                                    .ToList()
                }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.Message}";
            return response;
        }
    }

    public async Task<BaseResponseModel> UpdateBookReport(string id, UpdateBookReportViewModel request)
    {
        var response = new BaseResponseModel();

        var bookReportExist = await _unitOfWork.BookReports.ExistsAsync(c => c.Id == id);

        if (!bookReportExist)
        {
            response.Message = "Book report does not exist!";
            return response;
        }

        var bookReport = await _unitOfWork.BookReports.GetAsync(id);

        bookReport.AdditionalComment = request.AdditionalComment;

        try
        {
            await _unitOfWork.BookReports.UpdateAsync(bookReport);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Message = $"Could not update the book report: {ex.Message}";
            return response;
        }

        response.Message = "Book report updated successfully!";

        return response;
    }
}
