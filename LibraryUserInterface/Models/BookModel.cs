using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamLibrary.Models
{
    public class BookModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        
        public SelectList BookList { get; set; }
    }
}