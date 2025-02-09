namespace Identity.Domain.Dtos;
public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public DateTimeOffset? LastLoginTime { get; set; }
    public long RoleId { get; set; }
    public short Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

}
