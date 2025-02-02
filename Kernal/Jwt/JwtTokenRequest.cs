namespace Kernal.Jwt;

public class JwtTokenRequest
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
