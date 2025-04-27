using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace StudentGuide.DAL.Data.Models
{
    public class Material : Base
    {
        [Key]
        public int Id { get; set; }
        public String InstructorName { get; set; } = string.Empty;
        public string? Youtube { get; set; }
        public string? Drive { get; set; }
        [JsonIgnore]
        public virtual Course? Course { get; set; }
        [ForeignKey("Course")]
        public string? CourseCode { get; set; }
    }
}
