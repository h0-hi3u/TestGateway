using System;
using System.Collections.Generic;

namespace Model.EntityModels;

public partial class Product
{
    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public string CategoryId { get; set; }

    public string SupplierId { get; set; }

    public virtual Category Category { get; set; }

    public virtual Supplier Supplier { get; set; }
}
