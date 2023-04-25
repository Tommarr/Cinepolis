using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetAll();
        Payment Get(int id);
    }
}