namespace BadmintonBlast.Models.Dtos
{
    public class ReservationDTO
    {
        public int IdReservation { get; set; }

        public int? IdField { get; set; }

        public int? Idcustomer { get; set; }

        public int? Idhourlyrates { get; set; }

        public TimeOnly? Starttimerates { get; set; }

        public TimeOnly? Endtimerates { get; set; }

        public string? Namecustomer { get; set; }

        public string? Transactioncode { get; set; }

        public decimal? Price { get; set; }

        public string? Namefield { get; set; }

        public string? Fieldstatus { get; set; }

        public int? MissingSlots { get; set; }

        public int? Invoiceid { get; set; }
    }
}
