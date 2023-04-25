using OrderDomain.Models;

namespace OrderDomain.Services
{
    public interface IPaymentService
    {
        Payment PublishPayment(Payment payment);
    }
}