using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class Author : AuditedEntity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }
        [Required] [MaxLength(50)] public string SureName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}