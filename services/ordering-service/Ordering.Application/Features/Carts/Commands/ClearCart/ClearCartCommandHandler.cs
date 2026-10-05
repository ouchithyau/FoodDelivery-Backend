using MediatR;
using Ordering.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Carts.Commands.ClearCart;

public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
{
    private readonly ICartRepository _cartRepository;

    public ClearCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        await _cartRepository.ClearCartAsync(request.CustomerId);
        return true;
    }
}