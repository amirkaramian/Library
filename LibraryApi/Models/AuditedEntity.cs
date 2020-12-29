using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class AuditedEntity
    {
        [Key]
     public int Id { get; set; }
     public DateTime CreateAt { get; set;}
     public DateTime? EditAt { get; set; }
    }
}
