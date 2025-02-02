using Kernal.Models;

namespace CRM.Domain.Auth;

public class UserRole
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long RoleId { get; set; }
    public Role Role { get; set; }
}
