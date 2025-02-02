using Kernal.Models;

namespace CRM.Domain.Auth;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public List<UserRole> UserRoles { get; set; }
    public List<RolePermission> RolePermissions { get; set; }

}
