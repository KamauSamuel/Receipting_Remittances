using Domain.Payments;

namespace Application.Payments.GetReceipt;

public sealed class PaymentReceiptResponse
{
    public Guid PaymentId { get; set; }
    public string Payee { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime PaymentDate { get; set; }
    public string BankAccount { get; set; }
    public string RemittanceClassification { get; set; }
    public string PaymentPartner { get; set; }
    public PaymentDirection Direction { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime GeneratedAtUtc { get; set; }
}
