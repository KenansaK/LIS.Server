using CRM.Domain.Auth;
using Kernal.Contracts;
using Kernal.Interfaces;
using Kernal.Jwt;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace CRM.Application.Requests.Access
{
    public class LoginRequest : IRequest<object>
    {
        public LoginDTO LoginDTO { get; set; }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, object>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IRoleService _roleService;

        public LoginRequestHandler(
            IRepository<User> repository,
            IPasswordHasher<User> passwordHasher,
            IJwtService jwtService,
            IRoleService roleService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _roleService = roleService;

        }

        public async Task<object> Handle(LoginRequest request, CancellationToken cancellationToken)
        {

            var dto = request.LoginDTO;

            // Fetch the user by email
            var user = await _repository.GetByPropertyAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return "Invalid email or password.";
            }

            // Verify the password
            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (passwordVerification == PasswordVerificationResult.Failed)
            {
                return "Invalid email or password.";
            }

            // Fetch the roles associated with the user
            var roles = await GetUserRolesAsync(user.Id);

            // Create JwtTokenRequest and JwtOptions
            var tokenRequest = new JwtTokenRequest
            {
                UserId = user.Id.ToString(),
                Email = user.Email,
                Roles = roles
            };

            var jwtOptions = new JwtOptions
            {
                Secret = "super-duper-secret-key-that-should-also-be-fairly-long", // Replace with your configuration
                Issuer = "CRM",     // Replace with your configuration
                Audience = "account", // Replace with your configuration
                ExpirationMinutes = 30      // Replace with your configuration
            };

            // Generate the JWT token
            return _jwtService.GenerateToken(tokenRequest, jwtOptions);
        }

        private async Task<List<string>> GetUserRolesAsync(long userId)
        {
            // Fetch roles from your repository
            var roles = await _roleService.GetUserRolesAsyncClone(userId);

            return roles?.ToList() ?? new List<string>();
        }

    }
}
