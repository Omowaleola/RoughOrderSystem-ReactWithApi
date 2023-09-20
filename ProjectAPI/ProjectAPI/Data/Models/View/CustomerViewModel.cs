namespace ProjectAPI.Data.Models.View;

public class CustomerViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? UserRole { get; set; }
}