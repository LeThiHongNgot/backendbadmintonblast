using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Hourlyrate
{
    public int Idhourlyrates { get; set; }

    public TimeOnly? Starttimerates { get; set; }

    public TimeOnly? Endtimerates { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
