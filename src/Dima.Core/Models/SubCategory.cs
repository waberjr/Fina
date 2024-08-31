using Dima.Core.Models.Common;

namespace Dima.Core.Models;

public class SubCategory : BaseAuditableEntity
{
    public Category Category { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}