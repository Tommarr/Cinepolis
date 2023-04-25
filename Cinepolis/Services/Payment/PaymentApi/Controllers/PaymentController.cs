using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using OrderDomain.Models;
using OrderDomain.Services;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService paymentService)
        {
            _service = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string a = "dsfsdfdsf";

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentDto paymentDto)
        {
            Payment payment = new(paymentDto.OrderId, paymentDto.PaymentMethod, paymentDto.Amount);
            _service.PublishPayment(payment);

            return Ok(payment);
        }
    }
}
