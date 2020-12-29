using System.Collections.Generic;
using System.Threading.Tasks;
using ExamLibrary.Models;

namespace ExamLibrary.Service
{
    public interface IRequestService
    {
        Task<List<BookModel>> GetBookModel(int categoryId);
        Task<List<CategoryModel>> GetCategoryModel();
    }
}