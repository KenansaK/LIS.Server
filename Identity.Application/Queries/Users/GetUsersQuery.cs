using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Queries;

public class GetUsersQuery : IQuery<List<User>>
{
}
public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<User>>
{
    private readonly IRepository<User> _repository;

    public GetUsersQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<List<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        var db = _repository.Query()
            //.Where(x => x.IsEmailConfirmed == true)
            ;

        return await _repository.ToListAsync(db, cancellationToken);
    }
}
