using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Queries;

public class GetRolesQuery : IQuery<List<Role>>
{
}

public class GetRolesQueryHandler(IRepository<Role> repository) : IQueryHandler<GetRolesQuery, List<Role>>
{
    public async Task<List<Role>> Handle(GetRolesQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();

        // You can add additional filters if needed, such as checking for active roles or other criteria
        // db = db.Where(x => x.StatusId != 3);

        return await repository.ToListAsync(db, cancellationToken);
    }
}
