using PaymentApi.Context;
using System.Linq.Expressions;
using PaymentDomain.Repositories;
using PaymentDomain.Models;

namespace OrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PaymentContext _Context;
        private readonly ILogger<OrderRepository> _logger;
        public IQueryable<Order> Entities => _Context.Set<Order>();

        public OrderRepository(ILogger<OrderRepository> logger, PaymentContext orderContext)
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

        public IEnumerable<Order> GetAll()
        {
            return _Context.Set<Order>();
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
