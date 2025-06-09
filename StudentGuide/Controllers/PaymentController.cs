using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Payment;
using StudentGuide.BLL.Services.PaymentService;

namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IpaymentService _paymentService;
        public PaymentController(IpaymentService paymentService) { _paymentService = paymentService; }
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clientSecret= await _paymentService.CreateStripePaymentIntentAsync(dto);
            if (clientSecret == null)
            {
                BadRequest("Faild to payment");
            }
            return( Ok(dto));
        }
    }
}
