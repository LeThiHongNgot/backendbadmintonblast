using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Order
{
    public int Idorder { get; set; }

    public int Idbill { get; set; }

    public int? Idproduct { get; set; }

    public decimal? Price { get; set; }

    public string? Nameproduct { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public int? Quatity { get; set; }

    public DateTime? DateOrder { get; set; }

    public virtual Bill IdbillNavigation { get; set; } = null!;

    public virtual Product? IdproductNavigation { get; set; }
}
