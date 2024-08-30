using Dima.Core.Enums;

namespace Dima.Core.Models;

public class Transaction : BaseEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime? PaidOrReceivedAt { get; set; }

    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    public decimal Amount { get; set; }

    public Category Category { get; set; } = null!;
}