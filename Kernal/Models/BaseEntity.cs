namespace Kernal.Models;
public abstract class BaseEntity
{
    public long Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public short StatusId { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
