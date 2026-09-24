using Application.Abstractions.Data;
using Domain.Payments;
using Infrastructure.Database;
using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Payments;

internal sealed class PaymentDataProvider(ApplicationDbContext dbContext) : IPaymentDataProvider
{
    public async Task<PaymentDetails?> GetPaymentDetailsAsync(Guid remittanceId, CancellationToken cancellationToken = default)
    {
        PaymentDetailsRow? row = await dbContext.Database
            .SqlQuery<PaymentDetailsRow>($"EXEC dbo.GetRemittances @RemittanceId = {remittanceId}")
            .SingleOrDefaultAsync(cancellationToken);

        return row is null
            ? null
            : new PaymentDetails(
                row.Id,
                row.Payee,
                row.Amount,
                row.Currency,
                row.PaymentDate,
                row.BankAccount,
                row.RemittanceClassification,
                row.PaymentPartner,
                Enum.Parse<PaymentDirection>(row.Direction));
    }

    // Remittances results set produced by the "dbo.GetRemittances" stored procedure.
    private sealed record PaymentDetailsRow(
        Guid Id,
        string Payee,
        decimal Amount,
        string Currency,
        DateTime PaymentDate,
        string BankAccount,
        string RemittanceClassification,
        string PaymentPartner,
        string Direction);
}
