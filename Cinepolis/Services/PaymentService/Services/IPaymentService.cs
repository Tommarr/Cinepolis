using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IPaymentService
    {
        Payment ProcesPayment(Payment payment);
    }
}