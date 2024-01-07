using GlobalTicketManagement.Application.Features.Events.Commands.DeleteEvent;
using GlobalTicketManagement.Application.Features.Events.Commands.UpdateEvent;
using GlobalTicketManagement.Application.Features.Events.Queries.GetEventDetail;
using GlobalTicketManagement.Application.Features.Events.Queries.GetEventList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetAllEvents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EventListVm>>> GetAllEvents()
        {
            var dtos = await _mediator.Send(new GetEventsListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = nameof(GetEventById))]
        public async Task<ActionResult<EventDetailVm>> GetEventById(Guid id)
        {
            var getEventDetailQuery = new GetEventDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getEventDetailQuery));
        }

        [HttpPut(Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
        {
            await _mediator.Send(updateEventCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteEventCommand() { EventId = id };
            await _mediator.Send(deleteEventCommand);
            return NoContent();
        }

        //[HttpGet("export", Name = "ExportEvents")]
        //[FileResultContentType("text/csv")]
        //public async Task<FileResult> ExportEvents()
        //{
        //    var fileDto = await _mediator.Send(new GetEventsExportQuery());

        //    return File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName);
        //}
    }
}
