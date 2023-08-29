using AspNetCoreHero.ToastNotification.Abstractions;
using BookApp.Models.Book;
using BookApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers;

[Authorize]
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly ICategoryService _categoryService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly INotyfService _notyf;

    public BookController(
        IBookService bookService,
        ICategoryService categoryService,
        IHttpContextAccessor httpContextAccessor,
        INotyfService notyf)
    {
        _bookService = bookService;
        _categoryService = categoryService;
        _httpContextAccessor = httpContextAccessor;
        _notyf = notyf;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllBook();
        ViewData["Message"] = books.Message;
        ViewData["Status"] = books.Status;

        return View(books.Data);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.SelectCategories();
        ViewData["Message"] = "";
        ViewData["Status"] = false;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookViewModel request)
    {
        var response = await _bookService.Create(request);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return View();
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Book");
    }

    public async Task<IActionResult> GetBookByCategory(string id)
    {
        var response = await _bookService.GetBookByCategoryId(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View(response.Data);
    }

    public async Task<IActionResult> GetBookDetail(string id)
    {
        var response = await _bookService.GetBook(id);
        ViewData["Message"] = response.Message;
        ViewData["Status"] = response.Status;

        return View(response.Data);
    }

    public async Task<IActionResult> Update(string id)
    {
        var response = await _bookService.GetBook(id);

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, UpdateBookViewModel request)
    {
        var response = await _bookService.Update(id, request);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);

            return RedirectToAction("Index", "Home");
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Book");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBook([FromRoute] string id)
    {
        var response = await _bookService.Delete(id);

        if (response.Status is false)
        {
            _notyf.Error(response.Message);
            return View();
        }

        _notyf.Success(response.Message);

        return RedirectToAction("Index", "Book");
    }
}
