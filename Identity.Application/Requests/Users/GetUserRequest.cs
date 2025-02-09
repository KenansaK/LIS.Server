using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;
public class GetUserRequest : IRequest<Result<UserDto>>
{
    public long Id { get; set; }
}

public class GetUserRequesttHandler(Dispatcher dispatcher) : IRequestHandler<GetUserRequest, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await dispatcher.DispatchAsync(new GetUserQuery { Id = request.Id });

        if (user is null)
            return Result.NotFound("User Not Found");

        return Result.Success(user.ToDto());
    }
}