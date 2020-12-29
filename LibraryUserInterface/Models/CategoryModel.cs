using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamLibrary.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public SelectList CategoryList { get; set; }
        
    }
}