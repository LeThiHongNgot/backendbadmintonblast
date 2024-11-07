using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Racketspecification
{
    public int Idproduct { get; set; }

    public string? Weight { get; set; }

    public string? Balance { get; set; }

    public string? Flexibility { get; set; }

    public string? Framematerial { get; set; }

    public string? Shaftmaterial { get; set; }

    public string? Stringtension { get; set; }

    public string? Color { get; set; }

    public virtual Product IdproductNavigation { get; set; } = null!;
}
