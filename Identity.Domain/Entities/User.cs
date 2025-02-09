using Kernal.Models;

namespace Identity.Domain.Entities;
public class User : BaseEntity
{

    public string Username { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public DateTimeOffset? LastLoginTime { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
