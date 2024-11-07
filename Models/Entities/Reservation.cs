using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Reservation
{
    public int Idreservation { get; set; }

    public int? Idfield { get; set; }

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

    public int? Idinvoice { get; set; }

    public virtual Field? IdfieldNavigation { get; set; }

    public virtual Hourlyrate? IdhourlyratesNavigation { get; set; }

    public virtual Invoice? IdinvoiceNavigation { get; set; }
}
