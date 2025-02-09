using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;
public class GetUsersRequest : IRequest<Result<IEnumerable<UserDto>>> { }
public class GetUsersRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetUsersRequest, Result<IEnumerable<UserDto>>>
{
    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _dispatcher.DispatchAsync(new GetUsersQuery(), cancellationToken);

        if (users.Count > 0)
        {
            return Result.Success(users.ToDtos());
        }
        return Result.NotFound("No Users Found");
    }
}

