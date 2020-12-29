using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExamLibrary.Service
{
    public interface ILibraryService
    {

        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetBooksAsync(int? categoryId, CancellationToken cancellationToken);
        Task AddItems();
    }
}
