using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Order ProcesOrder(Order order);
    }
}