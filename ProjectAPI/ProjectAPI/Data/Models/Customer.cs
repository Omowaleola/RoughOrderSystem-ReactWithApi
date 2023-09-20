using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Data.Models;

[Table("Customer")]
[Microsoft.EntityFrameworkCore.Index("Email", Name = "UK_Customer_Name", IsUnique = true)]
public partial class Customer
{
    [Key]
    [StringLength(255)]
    public string Id { get; set; } = null!;

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int UserRole { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
