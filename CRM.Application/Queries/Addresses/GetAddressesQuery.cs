using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;
public class GetAddressesQuery : IQuery<List<Address>>
{
}
public class GetAddressesQueryHandler(IRepository<Address> repository) : IQueryHandler<GetAddressesQuery, List<Address>>
{
    public async Task<List<Address>> Handle(GetAddressesQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query().Where(x => x.StatusId != 3);

        return await repository.ToListAsync(db, cancellationToken);
    }
}