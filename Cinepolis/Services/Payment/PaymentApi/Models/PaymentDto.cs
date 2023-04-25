namespace PaymentApi.Models
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
