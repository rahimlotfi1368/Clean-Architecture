﻿using FluentValidation;
using FluentValidation.Validators;
using GlobalTicketManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidator(IEventRepository eventRepository)
         {
              _eventRepository = eventRepository;

              RuleFor(p=>p.Name)
                .NotEmpty().WithMessage("{PropertyName}  is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName}  must not exceed 50 characters.");

            RuleFor(p => p.Date)
                .NotEmpty().WithMessage("{PropertyName}  is required.")
                .NotNull()
                .GreaterThan(DateTime.Now).WithMessage("The Created {PropertyName} Should be greater then now");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("The {PropertyName} can not be Smaller then zero");

            RuleFor(e => e)
                .MustAsync(EventNameAndDateUnique)
                .WithMessage("An event with the same name and date already exists.");
        }

        private async Task<bool> EventNameAndDateUnique(CreateEventCommand e, CancellationToken token)
        {
            return !(await _eventRepository.IsEventNameAndDateUnique(e.Name, e.Date));
        }
    }
}
