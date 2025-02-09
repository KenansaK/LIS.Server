using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Queries;

public class GetRoleQuery : IQuery<Role?>
{
    public long Id { get; set; }
}

public class GetRoleQueryHandler(IRepository<Role> repository) : IQueryHandler<GetRoleQuery, Role?>
{
    public async Task<Role?> Handle(GetRoleQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();
        db = db
            .Where(x => x.Id == query.Id);

        // You can add additional filters like status checks if required (e.g., inactive roles)
        // db = db.Where(x => x.StatusId != 3);

        return await repository.FirstOrDefaultAsync(db, cancellationToken);
    }
}
