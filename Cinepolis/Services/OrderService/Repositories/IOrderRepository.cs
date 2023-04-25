using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order Get(int id);
    }
}