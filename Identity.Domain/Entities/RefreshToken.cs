using Kernal.Models;

namespace Identity.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public long UserId { get; set; } 
    public DateTimeOffset ExpiresOnUtc { get; set; }
    public bool IsRevoked { get; set; }
    public User User { get; set; }
    
}