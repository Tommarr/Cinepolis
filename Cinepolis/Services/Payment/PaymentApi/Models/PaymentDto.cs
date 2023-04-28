namespace PaymentApi.Models
{
    public class PaymentDto
    {
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
