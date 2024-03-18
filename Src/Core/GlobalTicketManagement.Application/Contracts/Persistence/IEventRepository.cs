using GlobalTicketManagement.Domain.Entities;

namespace GlobalTicketManagement.Application.Contracts.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsEventNameAndDateUnique(string name, DateTime dateTime);
    }
}
