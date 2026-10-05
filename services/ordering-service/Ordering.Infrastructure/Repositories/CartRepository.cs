using Microsoft.Extensions.Caching.Distributed;
using Ordering.Application.Interfaces;
using Ordering.Domain.Entities;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly IDistributedCache _redisCache;

    public CartRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<Cart?> GetCartAsync(Guid customerId)
    {
        var cartString = await _redisCache.GetStringAsync(customerId.ToString());
        if (string.IsNullOrEmpty(cartString))
            return null;

        return JsonSerializer.Deserialize<Cart>(cartString);
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3) 
        };

        var cartString = JsonSerializer.Serialize(cart);
        await _redisCache.SetStringAsync(cart.CustomerId.ToString(), cartString, options);
    }

    public async Task ClearCartAsync(Guid customerId)
    {
        await _redisCache.RemoveAsync(customerId.ToString());
    }
}