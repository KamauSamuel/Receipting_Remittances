using Application.Payments.GetReceipt;

namespace Application.Abstractions.Documents;

public interface IReceiptPdfGenerator
{
    byte[] Generate(PaymentReceiptResponse receipt);
}
