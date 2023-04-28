using PaymentDomain.Models;

namespace OrderApi.Services
{
    public interface IPaymentService
    {
        Payment PublishPayment(Payment payment);
        Payment CreatePayment(Payment order);
        IEnumerable<Payment> GetAllPayments();
    }
}