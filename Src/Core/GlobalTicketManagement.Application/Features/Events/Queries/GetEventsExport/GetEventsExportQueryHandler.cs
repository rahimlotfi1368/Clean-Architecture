using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Infrastructure;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;
        public GetEventsExportQueryHandler(IAsyncRepository<Event> eventRepository, IMapper mapper, ICsvExporter csvExporter)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _csvExporter = csvExporter;
        }
        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var eventList = (await _eventRepository.GetListAsync()).OrderBy(x => x.Date);

            var allEvents=_mapper.Map<List<EventExportDto>>(eventList);

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileVm
            {
                ContentType="text/csv",
                Data = fileData,
                EventExportFileName=$"{Guid.NewGuid()}.csv",
            };

            return eventExportFileDto;
        }
    }
}
