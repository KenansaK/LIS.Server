using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;
public class GetAddressQuery : IQuery<Address?>
{
    public long Id { get; set; }
}

public class GetAddressQueryHandler(IRepository<Address> repository) : IQueryHandler<GetAddressQuery, Address?>
{
    public async Task<Address?> Handle(GetAddressQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();
        db = db
            .Where(x => x.Id == query.Id)
            .Where(x => x.StatusId != 3);

        return await repository.FirstOrDefaultAsync(db, cancellationToken);

    }
}
