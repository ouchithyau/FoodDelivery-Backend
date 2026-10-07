using Microsoft.AspNetCore.Mvc;
using Stripe;
using Payment.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Payment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StripeWebhookController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<StripeWebhookController> _logger;

    private const string endpointSecret = "whsec_dummy_secret_key";

    public StripeWebhookController(IPaymentRepository paymentRepository, ILogger<StripeWebhookController> logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                if (paymentIntent != null && paymentIntent.Metadata != null && paymentIntent.Metadata.ContainsKey("OrderId"))
                {
                    _logger.LogInformation($"[Stripe Webhook] Payment Succeeded for Intent: {paymentIntent.Id}");

                    var orderIdString = paymentIntent.Metadata["OrderId"];

                    if (Guid.TryParse(orderIdString, out Guid orderId))
                    {
                        var transaction = await _paymentRepository.GetByOrderIdAsync(orderId);
                        if (transaction != null)
                        {
                            transaction.Status = "Completed";
                            await _paymentRepository.SaveChangesAsync();
                            _logger.LogInformation($"[Payment Service] Order {orderId} Payment Status successfully updated to 'Completed'!");
                        }
                    }
                }
            }

            return Ok();
        }
        catch (StripeException e)
        {
            _logger.LogError($"[Stripe Webhook] Error: {e.Message}");
            return BadRequest();
        }
    }
}