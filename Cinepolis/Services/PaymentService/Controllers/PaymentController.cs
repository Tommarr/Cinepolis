using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using PaymentService.Repositories;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repository;
        private readonly IPaymentService _service;

        public PaymentController(IPaymentRepository repository, IPaymentService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPost(Name = "PostPayment")]
        public Payment Post(Payment planning)
        {
            _service.ProcesPayment(planning);
            return planning;
        }
    }
}
