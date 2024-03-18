 using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GetEventDetailQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);

            var eventdetailDto = _mapper.Map<EventDetailVm>(@event);

            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

            eventdetailDto.Category = _mapper.Map<CategoryDto>(category);

            return eventdetailDto;
        }
    }
}
