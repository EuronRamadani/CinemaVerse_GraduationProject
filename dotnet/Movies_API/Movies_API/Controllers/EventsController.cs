using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Events;
using Movies.Services.Models.Common;
using Movies.Services.Services.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IEventService Eventservice, ILoggerFactory logger)
        {
            _eventService = Eventservice ?? throw new ArgumentNullException(nameof(Eventservice));
            _logger = logger.CreateLogger<EventsController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<EventListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId)
        {        
            var Events = await _eventService.GetAllAsync(cinemaId);
            return Ok(new ApiResponse<IList<EventListModel>>(Events));
        }

        [HttpGet]
        [Route("{EventId}")]
        [ProducesResponseType(typeof(ApiResponse<EventModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int EventId)
        {
            var Event = await _eventService.GetAsync(cinemaId, EventId);
            return Ok(new ApiResponse<EventModel>(Event));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<EventModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, [FromBody] EventCreateModel EventModel)
        {
            var Event = await _eventService.Create(cinemaId, EventModel);
            return CreatedAtAction(
                nameof(Post),
                new { EventId = Event.Id },
                new ApiResponse<EventModel>(Event)
            );
        }

        [HttpPut]
        [Route("{EventId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int EventId, [FromBody] EventCreateModel EventModel)
        {
            var Event = await _eventService.Update(EventId, EventModel);
            return Ok(new ApiResponse<EventModel>(Event));
        }

        [HttpPut]
        [Route("IncreaseAttendees/{EventId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GoToEvent(int EventId, [FromBody] EventCreateModel EventModel, int cinemaId)
        {
            var Event = await _eventService.GetAsync(cinemaId,EventId);

            Event.AttendeesNumber++;

            var EventModelNew=new EventCreateModel { AttendeesNumber = Event.AttendeesNumber, 
            Date=Event.Date,Description=Event.Description,IsPaid=Event.IsPaid,Price=Event.Price,Name=Event.Name};

            await _eventService.Update(EventId, EventModelNew);

            return Ok(new ApiResponse<EventModel>(Event));
        }

        [HttpDelete]
        [Route("{EventId}")]
        [ProducesResponseType(typeof(ApiResponse<EventModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int EventId)
        {
            var Event = await _eventService.Delete(EventId);
            return Ok(new ApiResponse<EventModel>(Event));
        }
    }
}

