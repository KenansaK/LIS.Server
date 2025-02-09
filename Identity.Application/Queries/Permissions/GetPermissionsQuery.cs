using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Queries;
public class GetPermissionsQuery : IQuery<List<Permission>>
{
}

public class GetPermissionsQueryHandler(IRepository<Permission> repository) : IQueryHandler<GetPermissionsQuery, List<Permission>>
{
    public async Task<List<Permission>> Handle(GetPermissionsQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();

        // You can add additional filters if needed, such as checking for active roles or other criteria
        // db = db.Where(x => x.StatusId != 3);

        return await repository.ToListAsync(db, cancellationToken);
    }
}

