using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderDomain.Models;
using OrderDomain.Services;
using PaymentApi.Models;
using System.Security.Claims;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            //ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            //if (identity == null || identity.Name == null)
            //    return Unauthorized();

            var orders = await _service.GetAllOrders();
            List<Order> ordersList = orders.ToList();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderDto orderDto)
        {
            await _service.CreateOrderAsync(new Order(orderDto.CustomerName));
            return Ok(orderDto);
        }
    }
}
