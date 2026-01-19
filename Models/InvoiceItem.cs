namespace PluralHealthDemoApp.Models
{
    public class InvoiceItem
    {
        public string ServiceName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}
