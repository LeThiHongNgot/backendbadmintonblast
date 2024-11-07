using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Brand
{
    public int Idbrand { get; set; }

    public string? Namebrand { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
