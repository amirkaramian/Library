using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExamLibrary.Service
{
    public class LibraryService : ILibraryService
    {
        LibararyDbContext _context;

        public LibraryService(LibararyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(int? categoryId, CancellationToken cancellationToken)
        {
            var book = _context.Books.AsQueryable();
            if (categoryId.HasValue)
                book = book.Where(c => c.CategoryId == categoryId.Value);

            return await book.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.GetCategory();
        }

        public async Task AddItems()
        {
            if (_context.Categories.Any())
                return;
            var author = new Author
            {
                Name = "amir",
                SureName = "karamian",
                BirthDate = DateTime.Now.AddYears(-30),
                CreateAt = DateTime.Now
            };
            var cat = new Category()
            {
                CategoryName = "Category1",
                CreateAt = DateTime.Now,
                Books = new List<Book>(),
            };
            cat.Books.Add(new Book()
            {
                Name = "book1",
                CreateAt = DateTime.Now,
                Isbn = "123",
                Author = author,
            });
            var cat2 = new Category()
            {
                CategoryName = "Category2",
                CreateAt = DateTime.Now,
                Books = new List<Book>(),
            };
            cat.Books.Add(new Book()
            {
                Name = "book2",
                CreateAt = DateTime.Now,
                Isbn = "1234",
                Author = author,
            });
            var cat3 = new Category()
            {
                CategoryName = "Category3",
                CreateAt = DateTime.Now,
                Books = new List<Book>(),
            };
            cat.Books.Add(new Book()
            {
                Name = "book3",
                CreateAt = DateTime.Now,
                Isbn = "1235",
                Author = author,
            });
            var list = new List<Category> {cat, cat2, cat3};
            await _context.Categories.AddRangeAsync(list, CancellationToken.None);
            try
            {
                 _context.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}