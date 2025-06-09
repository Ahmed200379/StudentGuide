using StudentGuide.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Dtos.Payment
{
    public class PaymentRequestDto
    {
        [Required]
        public string Reason { get; set; }
        public double Amount { get; set; }
        public int StudentId { get; set; }
        public PaymentMethods Method { get; set; }
    }
}
