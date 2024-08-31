namespace Dima.Core.Models.Common;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
}