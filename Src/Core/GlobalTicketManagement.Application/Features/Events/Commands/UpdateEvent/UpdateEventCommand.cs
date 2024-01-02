﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    /// <summary>
    /// Dont return any output
    /// so due to it we put mediator unit to set the output void
    /// </summary>
    public class UpdateEventCommand:IRequest<Unit>
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Artist { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
