using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Coupon
{
    public int Idcoupon { get; set; }

    public int? Promotion { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public int? Quality { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
