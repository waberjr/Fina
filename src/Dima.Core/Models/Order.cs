using Dima.Core.Enums;
using Dima.Core.Models.Common;

namespace Dima.Core.Models;

public class Order : BaseAuditableEntity
{
    public string Number { get; set; } = string.Empty;
    public string ExternalReference { get; set; } = string.Empty;
    public EOrderStatus Status { get; set; } = EOrderStatus.WaitingPayment;
    public EPaymentGateway Gateway { get; set; } = EPaymentGateway.Stripe;
    public decimal Amount { get; set; }
}