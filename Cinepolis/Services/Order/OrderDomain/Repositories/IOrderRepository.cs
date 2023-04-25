using OrderDomain.Models;
using System.Linq.Expressions;

namespace OrderDomain.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> Entities { get; }

        Order Add(Order entity);
        IEnumerable<Order> Find(Expression<Func<Order, bool>> query);
        Order Get(string id);
        IEnumerable<Order> GetAll();
        void Remove(Order entity);
        void Update(Order entity);
    }
}