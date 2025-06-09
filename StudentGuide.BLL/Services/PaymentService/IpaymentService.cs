using StudentGuide.BLL.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGuide.BLL.Services.PaymentService
{
   public interface IpaymentService
    {
        public Task<String> CreateStripePaymentIntentAsync(PaymentRequestDto paymentRequestDto);
    }
}
