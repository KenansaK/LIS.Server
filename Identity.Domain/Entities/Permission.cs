using Identity.Domain.Enums;
using Kernal.Models;

namespace Identity.Domain.Entities;

public class Permission : BaseEntity
{
    public string PermissionCode { get; set; }
    public string PermissionName { get; set; }
    public string module { get; set; }
    public List<RolePermission> RolePermissions { get; set; }

}
