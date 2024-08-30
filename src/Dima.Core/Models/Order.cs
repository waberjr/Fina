using Dima.Core.Enums;

namespace Dima.Core.Models;

public class Order : BaseEntity
{
    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string ExternalReference { get; set; } = string.Empty;
    public EOrderStatus Status { get; set; } = EOrderStatus.WaitingPayment;
    public EPaymentGateway Gateway { get; set; } = EPaymentGateway.Stripe;
    public decimal Amount { get; set; }
}