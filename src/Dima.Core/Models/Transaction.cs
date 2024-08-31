using Dima.Core.Enums;
using Dima.Core.Models.Common;

namespace Dima.Core.Models;

public class Transaction : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public DateTime? PaidOrReceivedAt { get; set; }

    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    public decimal Amount { get; set; }

    public Category Category { get; set; } = null!;
}