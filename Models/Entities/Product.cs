using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Product
{
    public int Idproduct { get; set; }

    public int? Idbrand { get; set; }

    public int? Idkindproduct { get; set; }

    public string? Nameproduct { get; set; }

    public string? Kindproduct { get; set; }

    public string? Namebrand { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public byte? Available { get; set; }

    public int? Deprice { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Brand? IdbrandNavigation { get; set; }

    public virtual Kindproduct? IdkindproductNavigation { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Preview> Previews { get; set; } = new List<Preview>();

    public virtual ICollection<Productstock> Productstocks { get; set; } = new List<Productstock>();

    public virtual Racketspecification? Racketspecification { get; set; }
}
