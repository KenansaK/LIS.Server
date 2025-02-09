using Identity.Domain.Entities;
using Kernal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Queries;

public class GetPermissionsForRoleQuery : IQuery<List<long>>
{
    public long RoleId { get; set; }
}

public class GetPermissionsForRoleQueryHandler(IRepository<RolePermission> repository) : IQueryHandler<GetPermissionsForRoleQuery, List<long>>
{
    public async Task<List<long>> Handle(GetPermissionsForRoleQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();

        // Perform filtering and selection on the queryable
        var permissionIds = await db
            .Where(x => x.RoleId == query.RoleId)
            .Select(x => x.PermissionId) // Select only the PermissionId
            .ToListAsync(cancellationToken);

        return permissionIds;
    }
}
