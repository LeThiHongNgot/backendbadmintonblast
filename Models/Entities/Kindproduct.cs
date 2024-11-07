using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Kindproduct
{
    public int Idkindproduct { get; set; }

    public string? Nameproduct { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
