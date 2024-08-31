namespace Dima.Core.Models.Common;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}