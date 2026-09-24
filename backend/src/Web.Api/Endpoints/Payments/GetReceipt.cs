using Application.Abstractions.Messaging;
using Application.Payments.GetReceipt;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Payments;

internal sealed class GetReceipt : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("payments/{id:guid}/receipt", async (
            Guid id,
            IQueryHandler<GetPaymentReceiptQuery, PaymentReceiptResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPaymentReceiptQuery(id);

            Result<PaymentReceiptResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Payments)
        .RequireAuthorization();
    }
}
