using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.Interfaces
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);
        Task AssignRoleToUserAsync(int userId, int roleId);
        Task AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task CreatePermissionAsync(string permissionName);
        Task<List<string>> GetUserPermissionsAsync(int userId);
        Task<List<string>> GetUserRolesAsyncClone(long userId);
    }

}

