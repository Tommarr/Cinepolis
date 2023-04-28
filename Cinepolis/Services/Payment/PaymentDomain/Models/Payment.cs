using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentDomain.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string? PaymentMethod { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public Payment()
        {
            Id = UniqueId.CreateUniqueId();
            OrderId = UniqueId.CreateUniqueId();
            PaymentMethod = null;
            Amount = 0;
            CreatedAt = DateTime.UtcNow;
        }

        public Payment(string orderId, string paymentMethod, decimal amount)
        {
            Id = UniqueId.CreateUniqueId();
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
