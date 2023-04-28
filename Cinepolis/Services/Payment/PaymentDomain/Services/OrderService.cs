using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentDomain.Models;
using PaymentDomain.Repositories;
using RabbitMQ.Client;
using System.Text;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger, IOrderRepository repository, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _repository = repository;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Order CreateOrder(Order order)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                Order createdOrder = orderRepository.Add(order);

                return createdOrder;
            }

            //Order createdOrder = _repository.Add(order);
            //return createdOrder;

        }

        public Order GetOrder(string id)
        {
            Order order = _repository.Get(id);
            return order;
        }

        IEnumerable<Order> IOrderService.GetAllOrders()
        {
            IEnumerable<Order> orders = _repository.GetAll();
            return orders;
        }
    }
}
