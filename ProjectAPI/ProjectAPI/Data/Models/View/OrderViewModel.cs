namespace ProjectAPI.Data.Models.View;

public class OrderSavingModel
{
    public string CustomerId { get; set; }
    public List<OrderProductModel> Products { get; set; }
}

public class OrderEditingModel
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public List<OrderProductModel> Products { get; set; }
}

public class OrderViewModel
{
    public string Id { get; set; }
    public string OrderNo { get; set; }
    public bool Paid { get; set; }
    public string Customer { get; set; }
    public double Total { get; set; }
    public DateTime CreatedOn { get; set; }
    public List<OrderProductViewModel> Products { get; set; }
}

public class OrderGridViewModel
{
    public string Id { get; set; }
    public string OrderNo { get; set; }
    public bool Paid { get; set; }
    public string Customer { get; set; }
    public double Total { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class OrderProductModel
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}
public class OrderProductViewModel
{
    
    public int Id { get; set; }
    public string ProductId { get; set; }
    public double ProductPrice { get; set; }
    public int Quantity { get; set; }
}