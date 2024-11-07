using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Preview
{
    public int Idpreview { get; set; }

    public int? Idproduct { get; set; }

    public int? Idcustomer { get; set; }

    public int? Preview1 { get; set; }

    public DateTime? Dateprevew { get; set; }

    public string? Comment { get; set; }

    public virtual Customer? IdcustomerNavigation { get; set; }

    public virtual Product? IdproductNavigation { get; set; }
}
