using Kernal.Jwt;
namespace Kernal.Interfaces;
public interface IJwtService
{
    string GenerateToken(JwtTokenRequest tokenRequest, JwtOptions options);
    string GenerateRefreshToken();
}

