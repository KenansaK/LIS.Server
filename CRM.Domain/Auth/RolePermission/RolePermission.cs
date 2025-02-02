using Kernal.Models;

namespace CRM.Domain.Auth;
public class RolePermission
{
    public long RoleId { get; set; }
    public Role Role { get; set; }

    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}

