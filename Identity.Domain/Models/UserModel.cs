namespace Identity.Domain.Models;

public class UserModel
{
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public long RoleId { get; set; }
    public short status { get; set; }
}
