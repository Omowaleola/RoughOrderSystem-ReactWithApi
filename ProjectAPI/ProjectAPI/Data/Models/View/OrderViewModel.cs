namespace ProjectAPI.Data.Models.View;

public class OrderViewModel
{
    public string Id { get; set; }
    public bool Paid { get; set; }
    public string CustomerId { get; set; }
    public double Total { get; set; }
    public DateTime? CreatedOn { get; set; }
    public List<string> Products { get; set; }
}