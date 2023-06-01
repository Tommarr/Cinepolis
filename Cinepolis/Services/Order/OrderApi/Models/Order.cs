using Microsoft.IdentityModel.Tokens;

namespace OrderApi.Models
{
    public class Order
    {


        public string Id { get; set; }

        public string? CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Order()
        {
            Id = UniqueId.CreateUniqueId();

            CustomerName = null;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }
        public Order(string customerName)
        {
            Id = UniqueId.CreateUniqueId();
            CustomerName = customerName;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }

    }
}
