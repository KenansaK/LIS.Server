using Kernal.Contracts;
using CRM.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using CRM.Infrastructure;
using Kernal.Interfaces;

namespace CRM.Infrastructure;

public class RoleService : IRoleService
{
    private readonly CRMDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(CRMDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRoleAsync(string roleName)
    {
        var role = new Role { Name = roleName };
        _context.Roles.Add(role);
        await _unitOfWork.SaveChanges();
    }

    public async Task AssignRoleToUserAsync(int userId, int roleId)
    {
        var userRole = new UserRole { UserId = userId, RoleId = roleId };
        _context.UserRoles.Add(userRole);
        await _unitOfWork.SaveChanges();
    }

    public async Task AssignPermissionToRoleAsync(int roleId, int permissionId)
    {
        var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
        _context.RolePermissions.Add(rolePermission);
        await _unitOfWork.SaveChanges();
    }

    public async Task CreatePermissionAsync(string permissionName)
    {
        var permission = new Permission { Name = permissionName };
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<string>> GetUserPermissionsAsync(int userId)
    {
        // Fetch permissions for a user based on their roles
        var permissions = await (from UserRoles in _context.UserRoles
                                 join RolePermissions in _context.RolePermissions
                                 on UserRoles.RoleId equals RolePermissions.RoleId
                                 join permission in _context.Permissions
                                 on RolePermissions.PermissionId equals permission.Id
                                 where UserRoles.UserId == userId
                                 select permission.Name).Distinct().ToListAsync();

        return permissions;
    }

    public async Task<List<string>> GetUserRolesAsyncClone(long userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .ToListAsync();
    }
}
