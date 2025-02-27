namespace Kernal.Middleware;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AuthorizePermissionAttribute : Attribute
{
    public string Permission { get; }

    public AuthorizePermissionAttribute(string permission)
    {
        Permission = permission;
    }
}