using Application.Abstractions.Documents;
using Application.Abstractions.Messaging;
using Application.Payments.GetReceipt;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Payments;

internal sealed class GetReceiptPdf : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("payments/{id:guid}/receipt/pdf", async (
            Guid id,
            IQueryHandler<GetPaymentReceiptQuery, PaymentReceiptResponse> handler,
            IReceiptPdfGenerator pdfGenerator,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPaymentReceiptQuery(id);

            Result<PaymentReceiptResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                receipt => Results.File(
                    pdfGenerator.Generate(receipt),
                    "application/pdf",
                    $"{receipt.ReceiptNumber}.pdf"),
                CustomResults.Problem);
        })
        .WithTags(Tags.Payments)
        .RequireAuthorization();
    }
}
