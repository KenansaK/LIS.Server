using Kernal.Models;

namespace CRM.Domain.Auth;

public class Permission : BaseEntity
{
    public string Name { get; set; }

    public List<RolePermission> RolePermissions { get; set; }

}
