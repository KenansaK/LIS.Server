using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.Interfaces
{
    public interface IRoleService
    {
        Task<List<string>> GetUserPermissionsAsync(int userId);
        Task<string> GetUserRoleAsync(long userId);
    }

}

