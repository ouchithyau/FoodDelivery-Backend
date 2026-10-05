using MediatR;
using Ordering.Application.Interfaces;
using Ordering.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Carts.Commands.AddToCart;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, bool>
{
    private readonly ICartRepository _cartRepository;

    public AddToCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartAsync(request.CustomerId);

        if (cart == null)
        {
            cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.UtcNow
            };
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.MenuItemId == request.MenuItemId);

        if (existingItem != null)
        {
           
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            
            cart.Items.Add(new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                MenuItemId = request.MenuItemId,
                ItemName = request.ItemName,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
            });
        }

        await _cartRepository.UpdateCartAsync(cart);
        return true;
    }
}