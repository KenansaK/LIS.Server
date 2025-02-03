using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;

public class GetBranchQuery : IQuery<Branch?>
{
    public long Id { get; set; }
}

public class GetBranchQueryHandler(IRepository<Branch> repository) : IQueryHandler<GetBranchQuery, Branch?>
{
    public async Task<Branch?> Handle(GetBranchQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();
        db = db
            .Where(x => x.Id == query.Id)
            .Where(x => x.StatusId != 3);

        return await repository.FirstOrDefaultAsync(db, cancellationToken);

    }
}