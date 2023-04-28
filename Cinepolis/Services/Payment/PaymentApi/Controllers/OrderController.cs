using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using OrderApi.Services;
using PaymentDomain.Models;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Order> orders = _service.GetAllOrders().ToList();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
           Order createdOrder = _service.CreateOrder(new());
           return Ok(createdOrder);
        }
    }
}
