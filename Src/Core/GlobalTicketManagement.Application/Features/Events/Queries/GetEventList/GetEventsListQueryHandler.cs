using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Events.Queries.GetEventList
{
    /// <summary>
    /// GetEventsListQuery is message type
    /// return a list of EventViewModel
    /// </summary>
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;

        public GetEventsListQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }
        public async Task<List<EventViewModel>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var allEvents = (await _eventRepository.GetListAsync()).OrderBy(x => x.Date);

            var eventlistVm = _mapper.Map<List<EventViewModel>>(allEvents);

            return eventlistVm;
        }
    }
}
