using MassTransit;
using Ordering.Application.Events;
using Microsoft.Extensions.Logging;
using Payment.Application.Interfaces;
using Payment.Domain.Entities;

namespace Payment.Application.Consumers;

public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ILogger<OrderPlacedEventConsumer> _logger;
    private readonly IPaymentGateway _paymentGateway;
    private readonly IPaymentRepository _paymentRepository;

    public OrderPlacedEventConsumer(
        ILogger<OrderPlacedEventConsumer> logger,
        IPaymentGateway paymentGateway,
        IPaymentRepository paymentRepository)
    {
        _logger = logger;
        _paymentGateway = paymentGateway;
        _paymentRepository = paymentRepository;
    }

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"[Payment Service] Processing Real-World Payment for Order ID: {message.OrderId}");

       
        var transaction = new PaymentTransaction(
            Guid.NewGuid(),
            message.OrderId,
            message.CustomerId,
            Guid.NewGuid(), 
            message.TotalPrice
        );

      
        transaction.CalculatePayouts(deliveryFee: 300m, platformCommissionPercentage: 20m);

        _logger.LogInformation($"[Payment Service] Breakdown -> SubTotal: {transaction.SubTotal}, Platform Fee: {transaction.PlatformFee}, Restaurant Gets: {transaction.RestaurantPayout}");

        
        var paymentIntentId = await _paymentGateway.CreatePaymentIntentAsync(message.OrderId, transaction.TotalAmount);

        if (!string.IsNullOrEmpty(paymentIntentId))
        {
            transaction.StripePaymentIntentId = paymentIntentId;
            transaction.Status = "AwaitingPayment"; 
            _logger.LogInformation($"[Payment Service] Stripe Intent Created: {paymentIntentId}. Awaiting user payment...");
        }
        else
        {
            transaction.Status = "Failed";
            _logger.LogWarning($"[Payment Service] Stripe Intent Creation Failed!");
        }

        
        await _paymentRepository.AddAsync(transaction);
        await _paymentRepository.SaveChangesAsync();
    }
}