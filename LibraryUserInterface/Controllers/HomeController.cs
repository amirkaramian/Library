using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamLibrary.Models;
using ExamLibrary.Service;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestService _requestService;

        public HomeController(ILogger<HomeController> logger, IRequestService requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _requestService.GetCategoryModel();
            ViewBag.VBCategoryist = new SelectList(list, "Id", "CategoryName");


            return View(await GetBook());
        }

        public async Task<PartialViewResult> Book(int categoryId)
        {
            return PartialView("_BookList", await GetBook(categoryId));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private async Task<BookModel> GetBook(int? categoryId = null)
        {
            var bookModel = new BookModel();
            if (categoryId.HasValue)
            {
                var list = await _requestService.GetBookModel(categoryId.Value);

                bookModel = new BookModel
                {
                    BookList = new SelectList(list,
                        "Id", "Name")
                };
            }
            else
            {
                var listBook = new List<BookModel>
                {
                    new BookModel() {Id = 0, Name = "no book", CategoryId = 0},
                };
                bookModel = new BookModel
                {
                    BookList = new SelectList(listBook,
                        "Id", "Name")
                };
            }

            return bookModel;
        }
    }
}