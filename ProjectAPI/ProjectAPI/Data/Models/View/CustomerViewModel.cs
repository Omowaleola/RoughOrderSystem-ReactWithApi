namespace ProjectAPI.Data.Models.View;

public class CustomerViewModel
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? UserRole { get; set; }
}

public class CustomerReturnModel
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class CustomerLoggedInModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int UserRole { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiry { get; set; }
}