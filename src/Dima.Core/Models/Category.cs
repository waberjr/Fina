using Dima.Core.Models.Common;

namespace Dima.Core.Models;

public class Category : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}