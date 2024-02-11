using AutoMapper;
using GlobalTicketManagement.Application.Contracts.Infrastructure;
using GlobalTicketManagement.Application.Contracts.Persistence;
using GlobalTicketManagement.Application.Models.Email;
using GlobalTicketManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GlobalTicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
        }
        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {


            var validator = new CreateEventCommandValidator(_eventRepository);

            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);


            var @event = _mapper.Map<Event>(request);

            @event = await _eventRepository.AddAsync(@event);

            var email = new Email
            {
                To = "sinuhe_1368@yahoo.com",
                Body = $"A new Event Was Started {request}",
                Subject = "A new Events was Created"
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                //ToDo This shouldn't stop the api from doing else this can be logged
                _logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
            }

            return @event.EventId;
        }

        // async Task<CreateEventCommandResponse> IRequestHandler<CreateEventCommand, CreateEventCommandResponse>.Handle(CreateEventCommand request, CancellationToken cancellationToken)
        //{
        //    var validator = new CreateEventCommandValidator(_eventRepository);

        //    var validationResult = await validator.ValidateAsync(request);

        //    if (validationResult.Errors.Count > 0)
        //    {
        //        var createEventCommandResponse = new CreateEventCommandResponse();
        //        //throw new Exceptions.ValidationException(validationResult);
        //        createEventCommandResponse.Success = false;
        //        createEventCommandResponse.ValidationErrors = new List<string>();
        //        foreach (var error in validationResult.Errors)
        //        {
        //            createEventCommandResponse.ValidationErrors.Add(error.ErrorMessage);
        //        }

        //        return createEventCommandResponse;
        //    }


        //    var @event = _mapper.Map<Event>(request);

        //    @event = await _eventRepository.AddAsync(@event);

        //    var email = new Email
        //    {
        //        To = "sinuhe_1368@yahoo.com",
        //        Body = $"A new Event Was Started {request}",
        //        Subject = "A new Events was Created"
        //    };

        //    try
        //    {
        //        await _emailService.SendEmailAsync(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        //ToDo This shouldn't stop the api from doing else this can be logged
        //    }

        //    return new CreateEventCommandResponse
        //    {
        //        Success = true,
        //         EventId=@event.EventId,
        //    };
        //}
    }
}
