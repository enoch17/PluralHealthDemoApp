using System.ComponentModel.DataAnnotations;

namespace PluralHealthDemoApp.Models
{
    public class CreateInvoiceRequest
    {
        public required int AppointmentId { get; set; } = default!;
        public required List<CreateInvoiceItemRequest> Items { get; set; } = new();
        public decimal DiscountPercentage { get; set; } = 0;
    }

    public class CreateInvoiceItemRequest
    {
        public required string ServiceName { get; set; } = default!;

        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
