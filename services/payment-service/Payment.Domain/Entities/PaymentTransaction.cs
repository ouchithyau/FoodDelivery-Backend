namespace Payment.Domain.Entities;

public class PaymentTransaction
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; } 

    
    public decimal TotalAmount { get; private set; }       
    public decimal DeliveryFee { get; private set; }      
    public decimal SubTotal { get; private set; }          
    public decimal PlatformFee { get; private set; }       
    public decimal RestaurantPayout { get; private set; }  

    public string Status { get; set; } = "Pending";
    public string? StripePaymentIntentId { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    
    public PaymentTransaction(Guid id, Guid orderId, Guid customerId, Guid restaurantId, decimal totalAmount)
    {
        Id = id;
        OrderId = orderId;
        CustomerId = customerId;
        RestaurantId = restaurantId;
        TotalAmount = totalAmount;
    }

    
    public void CalculatePayouts(decimal deliveryFee, decimal platformCommissionPercentage)
    {
        DeliveryFee = deliveryFee;
        SubTotal = TotalAmount - DeliveryFee;

        
        PlatformFee = SubTotal * (platformCommissionPercentage / 100);

       
        RestaurantPayout = SubTotal - PlatformFee;
    }
}