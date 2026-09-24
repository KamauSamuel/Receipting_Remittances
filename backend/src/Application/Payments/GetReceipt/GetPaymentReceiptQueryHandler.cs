using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Payments;
using SharedKernel;

namespace Application.Payments.GetReceipt;

internal sealed class GetPaymentReceiptQueryHandler(
    IPaymentDataProvider paymentDataProvider,
    IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetPaymentReceiptQuery, PaymentReceiptResponse>
{
    public async Task<Result<PaymentReceiptResponse>> Handle(GetPaymentReceiptQuery query, CancellationToken cancellationToken)
    {
        PaymentDetails? payment = await paymentDataProvider.GetPaymentDetailsAsync(query.PaymentId, cancellationToken);

        if (payment is null)
        {
            return Result.Failure<PaymentReceiptResponse>(PaymentErrors.NotFound(query.PaymentId));
        }

        return new PaymentReceiptResponse
        {
            PaymentId = payment.Id,
            Payee = payment.Payee,
            Amount = payment.Amount,
            Currency = payment.Currency,
            PaymentDate = payment.PaymentDate,
            BankAccount = payment.BankAccount,
            RemittanceClassification = payment.RemittanceClassification,
            PaymentPartner = payment.PaymentPartner,
            Direction = payment.Direction,
            ReceiptNumber = $"RCPT-{payment.Id:N}".ToUpperInvariant()[..14],
            GeneratedAtUtc = dateTimeProvider.UtcNow
        };
    }
}
