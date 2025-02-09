using Identity.Application.Commands;
using Identity.Application.Queries;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace Identity.Application.Requests;
public class DeleteUserRequest : IRequest<Result>
{
    public long Id { get; set; }
}
public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result>
{
    private readonly Dispatcher _dispatcher;

    public DeleteUserRequestHandler(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dispatcher.DispatchAsync(new GetUserQuery { Id = request.Id }, cancellationToken);

        if (user == null)
            return Result.NotFound("User not found");

        await _dispatcher.DispatchAsync(new DeleteUserCommand { User = user }, cancellationToken);

        return Result.Success("Successfully Deleted");
    }
}