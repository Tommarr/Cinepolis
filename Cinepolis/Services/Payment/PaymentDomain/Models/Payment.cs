using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDomain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public Payment(int orderId, string paymentMethod, decimal amount)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
