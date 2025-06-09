using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using StudentGuide.BLL.Dtos.Payment;
using StudentGuide.DAL.Data.Models;
using StudentGuide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.IO.RecyclableMemoryStreamManager;
namespace StudentGuide.BLL.Services.PaymentService
{
    public class PaymentService : IpaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork= unitOfWork;
        }
        public async Task<string> CreateStripePaymentIntentAsync(PaymentRequestDto paymentRequestDto)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long?)paymentRequestDto.Amount * 100,
                Currency = "EP",
                PaymentMethodTypes = new List<string> { "Card" }
            };
            var service = new PaymentIntentService();
            var intent=await service.CreateAsync(options);
            var payment = new Payment()
            {
                Amount = paymentRequestDto.Amount,
                Id= new Random().Next(123123,871346873),    
                Reason = intent.Id,
                StduentId = paymentRequestDto.StudentId,
                PaymentMethod = PaymentMethods.Vesa,
                Payment_Date = DateTime.Now,
                IsPayable = true
            };
            await _unitOfWork.PaymentRepo.AddAsync(payment);
           var isAdded= await _unitOfWork.Complete();
            if(isAdded==0)
            {
                throw new Exception("Payment Faild");
            }

            return intent.ClientSecret;
        }
        
        }
}
