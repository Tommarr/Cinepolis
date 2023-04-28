using Microsoft.IdentityModel.Tokens;

namespace PaymentDomain.Models
{
    public  class Order
    {


        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Order()
        {
            Id = UniqueId.CreateUniqueId();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }

        public Order(string id, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
