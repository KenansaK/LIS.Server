using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Kernal.Contracts;
using Kernal.Interfaces;
using Kernal.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Services;
public class LoginService : ILoginService
{
    private readonly IRepository<User> _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly Kernal.Interfaces.IRoleService _roleService;
    private readonly IConfiguration _configuration;

    public LoginService(
        IRepository<User> repository,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService,
        Kernal.Interfaces.IRoleService roleService,
        IConfiguration configuration)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _roleService = roleService;
        _configuration = configuration;
    }

    public async Task<LoginResponse> LoginAsync(LoginDTO dto)
    {
        // Fetch the user by email
        var user = await _repository.GetByPropertyAsync(u => u.Email == dto.Email);
        if (user == null)
        {
            return new LoginResponse
            {
                IsSuccessful = false,
                Message = "Invalid email or password."
            };
        }

        // Verify the password
        var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (passwordVerification == PasswordVerificationResult.Failed)
        {
            return new LoginResponse
            {
                IsSuccessful = false,
                Message = "Invalid email or password."
            };
        }

        // Fetch the roles associated with the user
        var roles = await GetUserRoleAsync(user.Id);

        // Load JWT options from configuration
        var jwtOptions = new JwtOptions
        {
            Secret = _configuration["Jwt:Secret"],
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            ExpirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"])
        };

        // Create JwtTokenRequest
        var tokenRequest = new JwtTokenRequest
        {
            UserId = user.Id.ToString(),
            Email = user.Email,
            Roles = roles
        };

        // Generate the JWT token
        var token = _jwtService.GenerateToken(tokenRequest, jwtOptions);

        return new LoginResponse
        {
            IsSuccessful = true,
            Token = token,
            Message = "Login successful."
        };
    }

    private async Task<List<string>> GetUserRoleAsync(long userId)
    {
        // Fetch the user's role directly using RoleId
        var roleName = await _repository.Query()
            .Where(u => u.Id == userId)
            .Select(u => u.Role.Name) // Assuming you have a navigation property for Role
            .FirstOrDefaultAsync();

        // Return the role name as a list or an empty list if no role is found
        return roleName != null ? new List<string> { roleName } : new List<string>();
    }




}
public class LoginResponse
{
    public bool IsSuccessful { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}