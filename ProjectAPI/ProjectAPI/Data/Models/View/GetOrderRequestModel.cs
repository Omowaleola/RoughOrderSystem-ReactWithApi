namespace ProjectAPI.Data.Models.View;

public class GetOrderRequestModel
{
    public List<string>? Ids { get; set; }
    public string? CustomerId { get; set; }
}