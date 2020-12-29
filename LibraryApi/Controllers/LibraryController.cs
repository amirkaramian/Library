using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExamLibrary.Service;
using LibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private ILibraryService _libraryService;
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILibraryService libraryService, ILogger<LibraryController> logger)
        {
            _libraryService = libraryService;
            _logger = logger;
            AddData();
        }

        [HttpGet("books/{categoryId}")]
        public async Task<IEnumerable<Book>> GetBooks(int categoryId, CancellationToken cancellationToken)
        {
            return await _libraryService.GetBooksAsync(categoryId, cancellationToken);
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> GetCategories(int categoryId, CancellationToken cancellationToken)
        {
            return await _libraryService.GetCategoriesAsync(cancellationToken);
        }

        private async Task AddData()
        {
            _libraryService.AddItems();
        }
    }
};