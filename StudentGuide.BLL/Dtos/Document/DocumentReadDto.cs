using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Document
{
   public class DocumentReadDto
    {
        public string Id { get; set; }=string.Empty;
        [Required]
        public String NameOfDocument { get; set; } = string.Empty;
        [Required]
        public string LinkOfDocument { get; set; } = string.Empty;

    }
}
