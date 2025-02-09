using Kernal.Models;

namespace Identity.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public List<User> Users { get; set; }
    public List<RolePermission> RolePermissions { get; set; }

}
