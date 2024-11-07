using System;
using System.Collections.Generic;

namespace BadmintonBlast.Models.Entities;

public partial class Bill
{
    public int Idbill { get; set; }

    public int? Idcustomer { get; set; }

    public DateTime Dateorder { get; set; }

    public string? Namecustomer { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? Totalamount { get; set; }

    public int? Status { get; set; }

    public string? Pay { get; set; }

    public string? Transactioncode { get; set; }

    public string? Message { get; set; }

    public int? Coupon { get; set; }

    public int? Idcoupon { get; set; }

    public virtual Coupon? IdcouponNavigation { get; set; }

    public virtual Customer? IdcustomerNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
