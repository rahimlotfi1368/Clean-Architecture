using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Persistence.Repository
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(DatabaseContext database):base(database) { }

        public Task<bool> IsEventNameAndDateUnique(string name, DateTime dateTime)
        {
            var matches=_dbContext.Events.Any(e=>e.Name.Equals(name) && e.Date.Date.Equals(dateTime));

            return Task.FromResult(matches);
        }
    }
}
