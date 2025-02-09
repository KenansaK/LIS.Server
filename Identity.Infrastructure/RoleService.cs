using Kernal.Contracts;
using Microsoft.EntityFrameworkCore;
using Kernal.Interfaces;
using Identity.Infrastructure;

namespace Identity.Application.Services;

public class RoleService : IRoleService
{
    private readonly IdentityDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IdentityDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }


    public async Task<List<string>> GetUserPermissionsAsync(int userId)
    {
        // Fetch the user's role
        var roleId = await _context.Users
                                   .Where(u => u.Id == userId)
                                   .Select(u => u.RoleId)
                                   .FirstOrDefaultAsync();

        if (roleId == 0)
            return new List<string>(); // No role found for the user, return empty list

        // Fetch permissions for the user's role
        var permissions = await (from rolePermission in _context.RolePermissions
                                 join permission in _context.Permissions
                                 on rolePermission.PermissionId equals permission.Id
                                 where rolePermission.RoleId == roleId
                                 select permission.PermissionCode).Distinct().ToListAsync();

        return permissions;
    }
    public async Task<string?> GetUserRoleAsync(long userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId) // Filter the user by their ID
            .Join(_context.Roles, u => u.RoleId, r => r.Id, (u, r) => r.Name) // Join with the Roles table to get the role name
            .FirstOrDefaultAsync(); // Get the role name or null if no match is found
    }


}
