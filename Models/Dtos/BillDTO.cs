namespace BadmintonBlast.Models.Dtos
{
    public class BillDTO
    {
        public int Idbill { get; set; }

        public int Idcustomer { get; set; }

        public DateTime Dateorder { get; set; }

        public string? Namecustomer { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public int? Totalamount { get; set; }

        public int? Status { get; set; }

        public string? Pay { get; set; }

        public string? Transactioncode { get; set; }

        public string? Message { get; set; }

        public int? Coupon { get; set; }

        public int? Idcoupon { get; set; }
    }
}
