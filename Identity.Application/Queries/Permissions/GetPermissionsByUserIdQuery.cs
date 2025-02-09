using Identity.Domain.Entities;
using Kernal.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Queries;

public class GetPermissionsByUserIdQuery : IQuery<List<string>>
{
    public long UserId { get; set; }
}
public class GetPermissionsByUserIdQueryHandler(IRepository<RolePermission> rolePermissionRepo,
    IRepository<User> userRepo,
    IRepository<Permission> permissionRepo) : IQueryHandler<GetPermissionsByUserIdQuery, List<string>>
{
    public async Task<List<string>> Handle(GetPermissionsByUserIdQuery query, CancellationToken cancellationToken = default)
    {

        var db = userRepo.Query();
        var userRole = await db
            .Where(x => x.Id == query.UserId)
            .Select(x => x.RoleId)
            .FirstOrDefaultAsync(cancellationToken);

        var db2 = rolePermissionRepo.Query();
        var permissionsIds = await db2
            .Where(x => x.RoleId == userRole)
            .Select(x => x.PermissionId)
            .ToListAsync(cancellationToken);

        var db3 = permissionRepo.Query();
        var permission = await db3
            .Where(x => permissionsIds.Contains(x.Id))
            .Select(x => x.PermissionCode)
            .ToListAsync(cancellationToken);

        return permission;
    }
}
