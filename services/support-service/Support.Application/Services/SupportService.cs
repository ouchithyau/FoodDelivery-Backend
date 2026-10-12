using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Domain.Entities;

namespace Support.Application.Services
{
    public interface ISupportService
    {
        Task<DisputeTicket> CreateTicketAsync(CreateTicketDto dto);
        Task<IEnumerable<DisputeTicket>> GetCustomerTicketsAsync(Guid customerId);
    }

    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _repository;

        public SupportService(ISupportRepository repository)
        {
            _repository = repository;
        }

        public async Task<DisputeTicket> CreateTicketAsync(CreateTicketDto dto)
        {
            var ticket = new DisputeTicket
            {
                OrderId = dto.OrderId,
                CustomerId = dto.CustomerId,
                Reason = dto.Reason,
                Description = dto.Description
            };

            await _repository.AddDisputeTicketAsync(ticket);
            await _repository.SaveChangesAsync();
            return ticket;
        }

        public async Task<IEnumerable<DisputeTicket>> GetCustomerTicketsAsync(Guid customerId)
        {
            return await _repository.GetTicketsByCustomerIdAsync(customerId);
        }
    }
}