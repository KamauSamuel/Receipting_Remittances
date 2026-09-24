using Domain.Payments;

namespace Application.Abstractions.Data;

// Mimics fetching payment data from an external stored procedure.
public interface IPaymentDataProvider
{
    Task<PaymentDetails?> GetPaymentDetailsAsync(Guid remittanceId, CancellationToken cancellationToken = default);
}
