using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderDomain.Models;
using OrderDomain.Services;
using PaymentApi.Models;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;

        public OrderController( IOrderService service, ILogger<OrderController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string a = "dsfsdfdsf";

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderDto orderDto)
        {
            _service.CreateOrder(new Order(orderDto.CustomerName));
             return Ok(orderDto);
        }
    }
}
