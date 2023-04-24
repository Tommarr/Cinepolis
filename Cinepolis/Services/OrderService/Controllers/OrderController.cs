using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using PaymentService.Repositories;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderService _service;
        public OrderController(IOrderRepository orderRepository, IOrderService orderService)
        {
            _repository = orderRepository;
            _service = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost(Name = "PostOrder")]
        public Order Post(Order order)
        {
            _service.ProcesOrder(order);
            return order;
        }

    }
}
