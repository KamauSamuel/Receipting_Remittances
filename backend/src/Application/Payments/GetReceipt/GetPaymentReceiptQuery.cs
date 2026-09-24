using Application.Abstractions.Messaging;

namespace Application.Payments.GetReceipt;

public sealed record GetPaymentReceiptQuery(Guid PaymentId) : IQuery<PaymentReceiptResponse>;
