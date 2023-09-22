using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Data.Models;

public partial class OrderProduct
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string OrderId { get; set; } = null!;

    [StringLength(255)]
    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderProducts")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderProducts")]
    public virtual Product Product { get; set; } = null!;
}
