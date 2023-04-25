using OrderService.Models;

namespace OrderService.Services
{
    public interface IPaymentService
    {
        Payment ProcesPayment(Payment payment);
    }
}