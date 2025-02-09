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
public class CreateUserRequest : IRequest<Result<UserDto>>
{
    public required UserModel Model { get; set; }
}
public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Result<UserDto>>
{
    private readonly Dispatcher _dispatcher;
    private readonly IPasswordHasher<User> _passwordHasher;

    public CreateUserRequestHandler(Dispatcher dispatcher, IPasswordHasher<User> passwordHasher)
    {
        _dispatcher = dispatcher;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserDto>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _dispatcher.DispatchAsync(new GetUserQuery { Email = request.Model.Email });
        if (existingUser != null)
        {
            return Result.Failure<UserDto>("Email already exists.");
        }

        var userEntity = request.Model.ToEntity();
        userEntity.PasswordHash = _passwordHasher.HashPassword(null, request.Model.Password);
        await _dispatcher.DispatchAsync(new AddUserCommand { User = userEntity });
        return Result.Success(userEntity.ToDto());
    }
}
