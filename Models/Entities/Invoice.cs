using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Invoice
{
    public int Idinvoice { get; set; }

    public int? Idcustomer { get; set; }

    public decimal? Totalamount { get; set; }

    public string? Paymentmethod { get; set; }

    public string? Customername { get; set; }

    public string? Customerphone { get; set; }

    public string? Transactioncode { get; set; }

    public DateTime? Reservationdate { get; set; }

    public bool? Status { get; set; }

    public virtual Customer? IdcustomerNavigation { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
