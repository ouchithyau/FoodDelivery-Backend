using MediatR;
using Ordering.Application.DTOs;
using System;

namespace Ordering.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;