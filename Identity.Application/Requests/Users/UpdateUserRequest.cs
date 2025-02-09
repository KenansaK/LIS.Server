using Identity.Application.Commands;
using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Identity.Domain.Models;
using Kernal.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;
public class UpdateUserRequest : IRequest<Result<UserDto>>
{
    public required long Id { get; set; }
    public required UserModel Model { get; set; }
}
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Result<UserDto>>
{
    private readonly Dispatcher _dispatcher;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UpdateUserRequestHandler(Dispatcher dispatcher, IPasswordHasher<User> passwordHasher)
    {
        _dispatcher = dispatcher;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dispatcher.DispatchAsync(new GetUserQuery { Id = request.Id });

        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        user.Username = request.Model.Username;
        user.FullName = request.Model.FullName;

        user.Email = request.Model.Email;
        user.PhoneNumber = request.Model.PhoneNumber;
        user.StatusId = request.Model.status;

        // Dispatch the command to update the user entity
        await _dispatcher.DispatchAsync(new UpdateUserCommand { User = user });

        return Result.Success(user.ToDto());
    }
}