using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Carts.Queries.GetCart;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private readonly ICartRepository _cartRepository;

    public GetCartQueryHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartAsync(request.CustomerId);

       
        if (cart == null)
        {
            return new CartDto(request.CustomerId, new List<CartItemDto>());
        }

        var cartItems = cart.Items.Select(i => new CartItemDto(
            i.MenuItemId,
            i.ItemName,
            i.UnitPrice,
            i.Quantity)).ToList();

        return new CartDto(cart.CustomerId, cartItems);
    }
}