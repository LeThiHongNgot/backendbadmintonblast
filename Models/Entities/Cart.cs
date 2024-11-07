using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Cart
{
    public int Idcart { get; set; }

    public int? Idproduct { get; set; }

    public int? Idcustomer { get; set; }

    public int? Quatity { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public virtual Customer? IdcustomerNavigation { get; set; }

    public virtual Product? IdproductNavigation { get; set; }
}
