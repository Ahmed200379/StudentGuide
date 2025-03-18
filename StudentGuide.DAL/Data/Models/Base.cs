using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
    public abstract class Base
    {
        [MaxLength(100), MinLength(12)]
        [Required]
        public String Name { get; set; } = string.Empty;
    }
}
