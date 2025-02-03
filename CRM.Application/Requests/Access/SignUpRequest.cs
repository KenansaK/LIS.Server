using CRM.Domain.Auth;
using FluentEmail.Core;
using Kernal.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CRM.Application.Requests.Access;

public class SignUpRequest : IRequest<object>
{
    public SignUpDTO SignUpDTO { get; set; }
}

public class SignUpRequestHandler : IRequestHandler<SignUpRequest, object>
{
    private readonly IRepository<User> _repository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public SignUpRequestHandler(IRepository<User> repository,
        IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<object> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        var dto = request.SignUpDTO;
        var existingUser = await _repository.GetByPropertyAsync(u => u.Email == dto.Email);
        if (existingUser != null)
        {
            throw new Exception("Email already exists.");
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(null, dto.Password)
        };

        await _repository.AddAsync(user);

        return new UserResponseDTO
        {
            Username = user.Username,
            Email = user.Email

        };
    }

}

