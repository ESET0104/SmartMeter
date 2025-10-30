namespace SmartMeterWeb.Models.Billing
{
    public class BillingDto
    {
        public class BillingRequestDto
        {
            public long ConsumerId { get; set; }
            public string MeterId { get; set; } = null!;
            public int Year { get; set; }
            public int Month { get; set; }
        }

        public class BillingResponseDto
        {
            public long BillId { get; set; }
            public long ConsumerId { get; set; }
            public string MeterId { get; set; } = null!;
            public decimal TotalUnitsConsumed { get; set; }
            public decimal BaseAmount { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public string BillingMonth { get; set; } = null!;
            public string PaymentStatus { get; set; } = "Unpaid";
        }
    }
}
