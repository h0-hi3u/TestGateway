using System;
using System.Collections.Generic;

namespace Model.EntityModels;

public partial class Supplier
{
    public string SupplierId { get; set; }

    public string SupplierName { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
