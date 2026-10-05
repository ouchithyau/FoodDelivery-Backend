using MediatR;
using Ordering.Application.DTOs;
using System;

namespace Ordering.Application.Features.Carts.Queries.GetCart;

public record GetCartQuery(Guid CustomerId) : IRequest<CartDto>;