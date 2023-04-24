using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IOrderService
    {
        Order ProcesOrder(Order order);
    }
}