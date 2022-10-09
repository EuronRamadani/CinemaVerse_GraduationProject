using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Common;
using Movies.Services.Models.Tickets;
using Movies.Services.Services.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/halls/{hallId}")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketService ticketService, ILoggerFactory logger)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _logger = logger.CreateLogger<TicketsController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("movieTimes/{movieTimeId}")]
        [ProducesResponseType(typeof(ApiResponse<IList<TicketModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int hallId, int movieTimeId)
        {
            var tickets = await _ticketService.GetAllAsync(cinemaId, hallId, movieTimeId);
            return Ok(new ApiResponse<IList<TicketModel>>(tickets));
        }        
        
        [HttpGet]
        [Route("user-tickets/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<IList<TicketModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string userId)
        {
            var userTickets = await _ticketService.GetAllUserTicketsAsync(userId);
            return Ok(new ApiResponse<IList<TicketModel>>(userTickets));
        }

        [HttpGet]
        [Route("tickets/{ticketId}")]
        [ProducesResponseType(typeof(ApiResponse<TicketModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSingle(int cinemaId, int hallId, int ticketId)
        {
            var ticket = await _ticketService.GetAsync(cinemaId, hallId, ticketId);
            return Ok(new ApiResponse<TicketModel>(ticket));
        }

        [HttpPut]
        [Route("tickets/movieTimes/{movieTimeId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int cinemaId, int hallId, int movieTimeId, [FromBody] ReserveTicketModel reserveModel)
        {
            var tickets = await _ticketService.ReserveTickets(cinemaId, hallId, movieTimeId, reserveModel.TicketsId, reserveModel.OwnerId);
            return Ok(new ApiResponse<List<TicketModel>>(tickets));
        }
    }
}
