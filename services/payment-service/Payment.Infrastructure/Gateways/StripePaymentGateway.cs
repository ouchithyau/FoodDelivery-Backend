using Stripe;
using Payment.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Payment.Infrastructure.Gateways;

public class StripePaymentGateway : IPaymentGateway
{
    private readonly ILogger<StripePaymentGateway> _logger;

    public StripePaymentGateway(ILogger<StripePaymentGateway> logger, IConfiguration configuration)
    {
        _logger = logger;
        
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"] ?? "sk_test_dummy_key_for_testing";
    }

    public async Task<string?> CreatePaymentIntentAsync(Guid orderId, decimal amount)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), 
                Currency = "lkr",
                Metadata = new Dictionary<string, string>
                {
                    { "OrderId", orderId.ToString() }
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            _logger.LogInformation($"[Stripe Gateway] Successfully created Payment Intent: {paymentIntent.Id}");
            return paymentIntent.Id;
        }
        catch (StripeException e)
        {
            _logger.LogError($"[Stripe Gateway] Stripe Error: {e.StripeError.Message}");
            return null;
        }
    }
}