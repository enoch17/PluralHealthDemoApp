namespace PluralHealthDemoApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = default!;
        public int AppointmentId { get; set; } = default!;
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
        public DateTime CreatedDate { get; set; }

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }

        public string Currency { get; set; } = "NGN";

        public List<InvoiceItem> Items { get; set; } = new();
    }

}
