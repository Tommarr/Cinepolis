using PaymentService.Models;

namespace PaymentService.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order Get(int id);
    }
}