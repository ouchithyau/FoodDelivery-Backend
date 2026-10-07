using System;
using System.Threading.Tasks;

namespace Payment.Application.Interfaces;

public interface IPaymentGateway
{
    Task<string?> CreatePaymentIntentAsync(Guid orderId, decimal amount);
}