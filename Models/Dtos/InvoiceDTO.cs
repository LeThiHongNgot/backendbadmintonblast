namespace BadmintonBlast.Models.Dtos
{
    public class InvoiceDTO
    {
        public int IdInvoice { get; set; } 

        public int Idcustomer { get; set; }

        public decimal? Totalamount { get; set; }

        public string? Paymentmethod { get; set; }

        public string? Customername { get; set; }

        public string? Customerphone { get; set; }

        public string? Transactioncode { get; set; }

        public DateTime? Reservationdate { get; set; }
        public bool? Status { get; set; }
    }
}
