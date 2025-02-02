using Kernal.Models;

namespace CRM.Domain.Auth;
public class User : BaseEntity
{

    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public List<UserRole> UserRoles { get; set; }
}

public class SignUpDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserResponseDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
}
