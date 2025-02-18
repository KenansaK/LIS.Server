using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Kernal.Contracts;
using Kernal.Helpers;
using Kernal.Interfaces;
using Kernal.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernel;


namespace Identity.Application.Services;
public class LoginService : ILoginService
{
    private readonly IRepository<User> _repository;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly Kernal.Interfaces.IRoleService _roleService;
    private readonly IConfiguration _configuration;
    private readonly Dispatcher _dispatcher;

    public LoginService(
        IRepository<User> repository,
        IRepository<RefreshToken> refreshTokenRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService,
        Kernal.Interfaces.IRoleService roleService,
        IConfiguration configuration,
        Dispatcher dispatcher)
    {
        _repository = repository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _roleService = roleService;
        _configuration = configuration;
        _dispatcher = dispatcher;
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
        var refreshtoken = _jwtService.GenerateRefreshToken();

        RefreshToken refreshToken = new RefreshToken
        {
            Token = refreshtoken,
            UserId = user.Id,
            ExpiresOnUtc = DateTime.UtcNow.AddDays((7)),
            CreatedAt = DateTime.UtcNow,
            CreatedBy = user.Email
        };
            await _refreshTokenRepository.AddAsync(refreshToken);
            
        return new LoginResponse
        {
            IsSuccessful = true,
            AccessToken = token,
            RefreshToken = refreshtoken,
            Message = "Login successful."
        };
        
       
    }

    public async Task<Result<RefreshTokenDto>> LoginUsingRefreshTokenAsync(string oldrefreshToken)
    {
        var gg = await _dispatcher.DispatchAsync(new GetRefreshTokenQuery{RefreshToken = oldrefreshToken});

        if (gg == null)
            return Result.Success("Invalid refresh token");
        if (gg.ExpiresOnUtc < DateTime.UtcNow)
        {
            gg.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(gg);
            return Result.Success("Please Login Again (Expired)");
        }

        if (gg.IsRevoked == false)
        {
            gg.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(gg);
        }

        var user = await _repository.GetByPropertyAsync(x=>x.Id == gg.UserId);
        var roles = await GetUserRoleAsync(user.Id);
      
        var jwtOptions = new JwtOptions
        {
            Secret = _configuration["Jwt:Secret"],
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            ExpirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"])
        };
        var tokenRequest = new JwtTokenRequest
        {
            UserId = user.Id.ToString(),
            Email = user.Email,
            Roles = roles
        };
        
        ///
        var accesstoken = _jwtService.GenerateToken(tokenRequest, jwtOptions);
        var refreshtoken = _jwtService.GenerateRefreshToken();

        RefreshToken refreshTokenRespone = new RefreshToken
        {
            Token = refreshtoken,
            UserId = user.Id,
            ExpiresOnUtc = DateTime.UtcNow.AddDays((7)),
            CreatedAt = DateTime.UtcNow,
            CreatedBy = user.Email
        };
        await _refreshTokenRepository.AddAsync(refreshTokenRespone);
            
        return new RefreshTokenDto
        {
            IsSuccessful = true,
            AccessToken = accesstoken,
            RefreshToken = refreshtoken,
            Message = "Tokens Generated successfully."
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
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; }= string.Empty;
    public string Message { get; set; } = string.Empty;
}