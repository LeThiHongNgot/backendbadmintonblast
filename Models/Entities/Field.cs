using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Field
{
    public int Idfield { get; set; }

    public string? Fieldname { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
