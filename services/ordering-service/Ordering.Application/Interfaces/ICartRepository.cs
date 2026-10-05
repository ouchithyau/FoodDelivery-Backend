using Ordering.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Ordering.Application.Interfaces;

public interface ICartRepository
{
    
    Task<Cart?> GetCartAsync(Guid customerId);

 
    Task UpdateCartAsync(Cart cart);

    
    Task ClearCartAsync(Guid customerId);
}