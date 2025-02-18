namespace Identity.Domain.Dtos;

public class RefreshTokenDto
{
    public bool IsSuccessful { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Message { get; set; } = string.Empty;
}