using System;
using System.Collections.Generic;

namespace First_core_project.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Discription { get; set; }

    public decimal? Price { get; set; }

    public int? Catid { get; set; }

    public string? Photo { get; set; }

    public string? Type { get; set; }

    public string? SupplierName { get; set; }

    public DateOnly? EntryDate { get; set; }

    public string? ReviewUrl { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Cat { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
