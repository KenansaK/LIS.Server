using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;
public class GetCustomersQuery : IQuery<List<Customer>>
{
}
public class GetOrdersQueryHandler(IRepository<Customer> repository) : IQueryHandler<GetCustomersQuery, List<Customer>>
{
    public async Task<List<Customer>> Handle(GetCustomersQuery query, CancellationToken cancellationToken = default)
    {
        var db = repository.Query().Where(x => x.StatusId != 3);

        return await repository.ToListAsync(db, cancellationToken);
    }
}

