using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
   public class Document:Base
    {
        [Key]   
        public String Id { get; set; }= Guid.NewGuid().ToString();
        [Required]
        public string Link { get; set; }=string.Empty;
    }
}
