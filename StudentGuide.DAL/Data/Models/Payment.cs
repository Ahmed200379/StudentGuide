using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.DAL.Data.Models
{
    public enum PaymentMethod
    {
        Vodaphone,
        Fawry,
        Vesa
    }
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), MinLength(5)]
        public string Reason { get; set; } = string.Empty;
        [Range(0, 10000)]
        public Double Amount { get; set; }
        public DateTime Payment_Date { get; set; }
        [EnumDataType(typeof(PaymentMethod))]
        [Column(TypeName = "nvarchar(20)")]
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsPayable { get; set; } = false;
        public Student stduent { get; set; }
        [ForeignKey("Stduent")]
        public int StduentId { get; set; }
    }
}
