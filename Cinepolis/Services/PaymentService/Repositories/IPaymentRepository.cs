using PaymentService.Models;

namespace PaymentService.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetAll();
        Payment Get(int id);
    }
}