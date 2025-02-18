using Identity.Domain.Entities;
using Kernal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Queries;

public class GetRefreshTokenQuery : IQuery<RefreshToken>
{
    public string RefreshToken { get; set; }
}

public class GetRefreshTokenQueryHandler(IRepository<RefreshToken> repository) : IQueryHandler<GetRefreshTokenQuery, RefreshToken>
{

    public async Task<RefreshToken> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var db = repository.Query();
        db = db
            .Where(t => t.Token == request.RefreshToken)
            .Where(t => t.IsRevoked != true);
        
        return await repository.FirstOrDefaultAsync(db,cancellationToken);
    }
}