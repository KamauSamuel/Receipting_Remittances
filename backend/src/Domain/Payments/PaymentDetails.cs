namespace Domain.Payments;

// Shape returned by the "dbo.GetPaymentDetails" stored procedure.
public sealed record PaymentDetails(
    Guid Id,
    string Payee,
    decimal Amount,
    string Currency,
    DateTime PaymentDate,
    string BankAccount,
    string RemittanceClassification,
    string PaymentPartner,
    PaymentDirection Direction);
