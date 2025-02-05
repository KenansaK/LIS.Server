using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;
public class GetAddressesByBranchIdQuery : IQuery<IEnumerable<Address?>>  // Return multiple addresses since a branch can have multiple addresses
{
    public long BranchId { get; set; }
}

public class GetAddressesByBranchIdQueryHandler : IQueryHandler<GetAddressesByBranchIdQuery, IEnumerable<Address?>>
{
    private readonly IRepository<Address> _addressRepository;

    public GetAddressesByBranchIdQueryHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<IEnumerable<Address?>> Handle(GetAddressesByBranchIdQuery query, CancellationToken cancellationToken = default)
    {
        var db = _addressRepository.Query()
            .Where(a => a.BranchId == query.BranchId);

        return await _addressRepository.ToListAsync(db, cancellationToken);
    }
}


