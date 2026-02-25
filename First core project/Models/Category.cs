using System;
using System.Collections.Generic;

namespace First_core_project.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Photo { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
