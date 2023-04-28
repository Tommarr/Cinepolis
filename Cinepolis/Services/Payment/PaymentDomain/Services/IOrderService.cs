using PaymentDomain.Models;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Order GetOrder(string id);
        IEnumerable<Order> GetAllOrders();
        public Order CreateOrder(Order order);
    }
}