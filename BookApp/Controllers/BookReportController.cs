using AspNetCoreHero.ToastNotification.Abstractions;
using BookApp.Models.BookReport;
using BookApp.Service.Implementations;
using BookApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    public class BookReportController : Controller
    {
        private readonly IBookReportService _bookReportService;
        private readonly INotyfService _notyf;
        private readonly IFlagService _flagService;

        public BookReportController(
            IBookReportService bookReportService,
            IFlagService flagService,
            INotyfService notyf)
        {
            _bookReportService = bookReportService;
            _notyf = notyf;
            _flagService = flagService;
        }

        //public IActionResult Index()
        //{
        //    var response = _questionReportService.GetAllQuestionReport();
        //    ViewBag.Message = response.Message;
        //    ViewBag.status = response.Status;

        //    return View(response.Data);
        //}

        //    return RedirectToAction("Index", "QuestionReport");
        //}

        public async Task<IActionResult> ReportBook()
        {
            ViewBag.FlagLists = await _flagService.SelectFlags();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReportBook(CreateBookReportViewModel Report)
        {
            var response = await _bookReportService.CreateBookReport(Report);

            if (response.Status is false)
            {
                return View(response);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetBookReport(string id)
        {
            var response = await _bookReportService.GetBookReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return RedirectToAction("Index", "Book");
            }

            return View(response.Data);
        }

        public async Task<IActionResult> GetBookReports(string id)
        {
            var response = await _bookReportService.GetBookReports(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Book");
            }

            return View(response.Data);
        }

        public async Task<IActionResult> UpdateBookReport(string id)
        {
            var response = await _bookReportService.GetBookReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Book");
            }

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBookReport(string id, UpdateBookReportViewModel request)
        {
            var response = await _bookReportService.UpdateBookReport(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Book");
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBookReport(string id)
        {
            var response = await _bookReportService.DeleteBookReport(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return RedirectToAction("Index", "Book");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Book");
        }
    }
}
