using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Customer
{
    public int Idcustomer { get; set; }

    public string? Namecustomer { get; set; }

    public string? ImageCustomer { get; set; }

    public string? Phone { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Village { get; set; }

    public string? Hamlet { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public bool? Status { get; set; }

    public string? Role { get; set; }

    public DateOnly? BirthDay { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Otp? EmailNavigation { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Preview> Previews { get; set; } = new List<Preview>();
}
