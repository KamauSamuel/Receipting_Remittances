using SharedKernel;

namespace Domain.Payments;

public static class PaymentErrors
{
    public static Error NotFound(Guid paymentId) => Error.NotFound(
        "Payments.NotFound",
        $"The payment with the Id = '{paymentId}' was not found");
}
