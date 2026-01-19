namespace PluralHealthDemoApp.Models
{
    public class Patient
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNo { get; set; }
        public Decimal WalletBalance { get; set; } = new Decimal(0);
        public string WalletCurrency { get; set; } = "NGN";
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
