using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using OrderApi.Services;
using PaymentDomain.Models;

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
            List<Payment> orders = _service.GetAllPayments().ToList();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentDto paymentDto)
        {
            Payment payment = _service.CreatePayment(new(paymentDto.OrderId, paymentDto.PaymentMethod, paymentDto.Amount));
            

            _service.PublishPayment(payment);

            return Ok(payment);
        }
    }
}
