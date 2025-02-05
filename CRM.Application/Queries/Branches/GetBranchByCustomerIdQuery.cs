using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Queries;

public class GetBranchByCustomerIdQuery : IQuery<IEnumerable<Branch?>>
{
    public long CustomerId { get; set; }
}

public class GetBranchByCustomerIdQueryHandler : IQueryHandler<GetBranchByCustomerIdQuery, IEnumerable<Branch?>>
{
    private readonly IRepository<Branch> _branchRepository;

    public GetBranchByCustomerIdQueryHandler(IRepository<Branch> branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<IEnumerable<Branch?>> Handle(GetBranchByCustomerIdQuery query, CancellationToken cancellationToken = default)
    {
        var db = _branchRepository.Query()
            .Where(b => b.CustomerId == query.CustomerId)
            .Where(b => b.StatusId != 3);


        return await _branchRepository.ToListAsync(db, cancellationToken);
    }
}

