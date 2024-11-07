using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Image
{
    public int Idproduct { get; set; }

    public string? Image4 { get; set; }

    public int Id { get; set; }

    public virtual Product IdproductNavigation { get; set; } = null!;
}
