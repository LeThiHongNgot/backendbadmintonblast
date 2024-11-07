namespace BadmintonBlast.Models.Dtos
{
    public class StatisticalResult
    {
        public int TotalBills { get; set; }
        public int CompletedBills { get; set; }
        public int TotalRevenue { get; set; }

    }
    public class StatisticalInvoice
    {
        public int TotalInvoice { get; set; }
        public int CompletedInvoice { get; set; }
        public int TotalRevenue { get; set; }

    }
    public class ProductSalesDTO
    {
        public int? ProductId { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
    }

}
