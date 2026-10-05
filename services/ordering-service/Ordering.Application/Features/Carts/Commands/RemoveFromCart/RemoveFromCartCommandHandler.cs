using MediatR;
using Ordering.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Carts.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, bool>
{
    private readonly ICartRepository _cartRepository;

    public RemoveFromCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<bool> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartAsync(request.CustomerId);
        if (cart == null) return false;

        var itemToRemove = cart.Items.FirstOrDefault(i => i.MenuItemId == request.MenuItemId);
        if (itemToRemove != null)
        {
            cart.Items.Remove(itemToRemove);
            await _cartRepository.UpdateCartAsync(cart);
        }

        return true;
    }
}