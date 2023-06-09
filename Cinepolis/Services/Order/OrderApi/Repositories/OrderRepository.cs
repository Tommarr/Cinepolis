﻿using OrderDomain.Models;
using OrderApi.Context;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using OrderDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _Context;
        private readonly ILogger<OrderRepository> _logger;
        public IQueryable<Order> Entities => _Context.Set<Order>();

        public OrderRepository(ILogger<OrderRepository> logger, OrderContext orderContext)
        {
            _logger = logger;
            _Context = orderContext;
        }

        public Order Add(Order entity)
        {
            _Context.Set<Order>().Add(entity);
            _Context.SaveChanges();
            return entity;
        }

        public IEnumerable<Order> Find(Expression<Func<Order, bool>> query)
        {
            return _Context.Set<Order>().Where(query);
        }

        public Order Get(string id)
        {
            return _Context.Set<Order>().Find(id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _Context.Set<Order>().ToListAsync();
            return orders;
        }

        public void Remove(Order entity)
        {
            _Context.Set<Order>().Remove(entity);
        }

        public void Update(Order entity)
        {
            _Context.Set<Order>().Update(entity);
        }
    }
}
