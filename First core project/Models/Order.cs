using System;
using System.Collections.Generic;

namespace First_core_project.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatAt { get; set; }

    public DateTime? PaidAt { get; set; }
    public object OrderItems { get; internal set; }
}
