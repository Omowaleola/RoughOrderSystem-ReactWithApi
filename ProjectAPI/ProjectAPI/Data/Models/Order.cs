using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Data.Models;

[Microsoft.EntityFrameworkCore.Index("OrderNo", Name = "UK_Orders_OrderNo", IsUnique = true)]
public partial class Order
{
    [Key]
    [StringLength(255)]
    public string Id { get; set; } = null!;

    [StringLength(300)]
    public string OrderNo { get; set; } = null!;

    public bool Paid { get; set; }

    [StringLength(255)]
    public string CustomerId { get; set; } = null!;

    public double Total { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
