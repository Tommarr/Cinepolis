using OrderDomain.Models;

namespace OrderDomain.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}