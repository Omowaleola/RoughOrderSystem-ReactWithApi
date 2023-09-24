using System.Collections.Generic;

namespace ProjectAPI.Data.Models.View;

public class ProductViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
}

public class ProductSavingModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
}
public class RequestModel
{
    public List<string>? Ids { get; set;}
}
public class ProductDeletingModel
{
    public List<string> Ids { get; set;}
}