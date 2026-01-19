namespace PluralHealthDemoApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaidAt { get; set; }
        public string Clinic { get; set; } = string.Empty;
        public string ReceivedBy { get; set; } = string.Empty;
    }

}
