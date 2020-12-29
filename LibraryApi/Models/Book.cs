using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class Book : AuditedEntity
    {
        [Required] [MaxLength(50)] 
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Isbn { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
    }
}