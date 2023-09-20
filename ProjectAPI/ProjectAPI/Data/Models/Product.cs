using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Data.Models;

[Table("Product")]
[Microsoft.EntityFrameworkCore.Index("Name", Name = "UK_Product_Name", IsUnique = true)]
public partial class Product
{
    [Key]
    [StringLength(255)]
    public string Id { get; set; } = null!;

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
