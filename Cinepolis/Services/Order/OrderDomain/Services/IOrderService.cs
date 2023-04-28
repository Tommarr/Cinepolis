using OrderDomain.Models;

namespace OrderDomain.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        IEnumerable<Order> GetAllOrders();
    }
}