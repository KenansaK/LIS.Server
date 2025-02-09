namespace Identity.Domain.Models;
public class PermissionModel
{
    public long Id { get; set; }
    public string PermissionCode { get; set; }
    public string PermissionName { get; set; }
    public string Module { get; set; }

    public short Status { get; set; }
}
