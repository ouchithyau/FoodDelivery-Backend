using MassTransit;
using Logistics.Application.Events;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Logistics.Application.Consumers;

public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly ILogger<PaymentCompletedEventConsumer> _logger;

    public PaymentCompletedEventConsumer(
        IDeliveryRepository deliveryRepository,
        ILogger<PaymentCompletedEventConsumer> logger)
    {
        _deliveryRepository = deliveryRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"[Logistics Service] Payment completed for Order: {message.OrderId}. Creating delivery...");

        
        var delivery = new Delivery(
            Guid.NewGuid(),
            message.OrderId,
            message.PickupAddress,
            message.DropoffAddress
        );

        await _deliveryRepository.AddAsync(delivery);
        await _deliveryRepository.SaveChangesAsync();

        _logger.LogInformation($"[Logistics Service] Delivery {delivery.Id} created successfully. Status: {delivery.Status}");
    }
}