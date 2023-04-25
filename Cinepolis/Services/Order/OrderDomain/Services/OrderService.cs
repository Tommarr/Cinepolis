using Microsoft.Extensions.Logging;
using OrderDomain.Models;
using OrderDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDomain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        //private readonly IRepository<Room> _repository;
        private readonly ILogger<OrderService> _logger;
        //private readonly THub _roomHub;
        public OrderService(IOrderRepository roomRepository, ILogger<OrderService> logger)
        {
            _logger = logger;
            _repository = roomRepository;
        }

        public Order CreateOrder(Order order)
        {
            Order createdOrder = _repository.Add(order);
            return createdOrder;
        }
    }
}
