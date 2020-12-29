using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Models
{
    public class Category : AuditedEntity
    {
        [Required]
        [MaxLength(50)]
        
        public string CategoryName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
