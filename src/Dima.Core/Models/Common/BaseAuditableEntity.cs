namespace Dima.Core.Models.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}