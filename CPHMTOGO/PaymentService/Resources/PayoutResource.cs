namespace PaymentService.Resources
{
    public record PayoutResource(
    string Currency, long  Amount,string Description);
}