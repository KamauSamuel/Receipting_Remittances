using Application.Abstractions.Documents;
using Application.Payments.GetReceipt;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.Payments;

internal sealed class ReceiptPdfGenerator : IReceiptPdfGenerator
{
    public byte[] Generate(PaymentReceiptResponse receipt)
    {
        IDocument document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Text("Payment Receipt").FontSize(20).Bold();

                page.Content()
                    .PaddingVertical(15)
                    .Column(column =>
                    {
                        column.Spacing(8);

                        column.Item().Text($"Receipt Number: {receipt.ReceiptNumber}");
                        column.Item().Text($"Payee: {receipt.Payee}");
                        column.Item().Text($"Amount: {receipt.Amount:0.00} {receipt.Currency}");
                        column.Item().Text($"Payment Date: {receipt.PaymentDate:yyyy-MM-dd}");
                        column.Item().Text($"Bank Account: {receipt.BankAccount}");
                        column.Item().Text($"Remittance Classification: {receipt.RemittanceClassification}");
                        column.Item().Text($"Payment Partner: {receipt.PaymentPartner}");
                        column.Item().Text($"Direction: {receipt.Direction}");
                        column.Item().Text($"Generated At (UTC): {receipt.GeneratedAtUtc:yyyy-MM-dd HH:mm:ss}");
                    });

                page.Footer().AlignCenter().Text("Thank you for your payment.");
            });
        });

        return document.GeneratePdf();
    }
}
