using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Queries;

public class GetUserQuery : IQuery<User?>
{
    public long? Id { get; set; }
    public string? Email { get; set; }

    // Constructor to allow initialization of both Id and Email
    public GetUserQuery(long? id = null, string? email = null)
    {
        Id = id;
        Email = email;
    }
}
public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User?>
{
    private readonly IRepository<User> _repository;

    public GetUserQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> Handle(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        var db = _repository.Query();

        // Apply filters based on the provided parameters (Id or Email)
        if (query.Id.HasValue)
        {
            db = db.Where(x => x.Id == query.Id.Value);
        }

        if (!string.IsNullOrEmpty(query.Email))
        {
            db = db.Where(x => x.Email == query.Email);
        }

        // Get the user based on the query parameters
        return await _repository.FirstOrDefaultAsync(db, cancellationToken);
    }
}