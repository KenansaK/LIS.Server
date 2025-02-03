using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;

public class GetBranchesQuery : IQuery<List<Branch>>
{
}
public class GetBranchesQueryHandler(IRepository<Branch> repository) : IQueryHandler<GetBranchesQuery, List<Branch>>
{
    public async Task<List<Branch>> Handle(GetBranchesQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query().Where(x => x.StatusId != 3);

        return await repository.ToListAsync(db, cancellationToken);
    }
}