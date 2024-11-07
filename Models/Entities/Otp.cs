using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Otp
{
    public string Email { get; set; } = null!;

    public string Otp1 { get; set; } = null!;

    public DateTime Expiration { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsUsed { get; set; }

    public int Attempts { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
