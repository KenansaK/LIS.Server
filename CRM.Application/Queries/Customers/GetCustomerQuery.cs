using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;
public class GetCustomerQuery : IQuery<Customer?>
{
    public long Id { get; set; }
}

public class GetCustomerQueryHandler(IRepository<Customer> repository) : IQueryHandler<GetCustomerQuery, Customer?>
{
    public async Task<Customer?> Handle(GetCustomerQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query();
        db = db
            .Where(x => x.Id == query.Id)
            .Where(x => x.StatusId != 3);

        return await repository.FirstOrDefaultAsync(db, cancellationToken);

    }
}
