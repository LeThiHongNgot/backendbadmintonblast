using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Productstock
{
    public int Id { get; set; }

    public int Idproduct { get; set; }

    public string? Namecolor { get; set; }

    public string? Namesize { get; set; }

    public int? Quatity { get; set; }

    public virtual Product IdproductNavigation { get; set; } = null!;
}
