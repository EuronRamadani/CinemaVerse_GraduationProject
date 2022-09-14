using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Cinemas;
using Movies.Services.Models.Common;
using Movies.Services.Models.Halls;
using Movies.Services.Services.Cinemas;
using Movies.Services.Services.Halls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/halls")]
    public class HallsController : ControllerBase
    {
        private readonly IHallService _hallService;
        private readonly ILogger<HallsController> _logger;

        public HallsController(IHallService hallService, ILoggerFactory logger)
        {
            _hallService = hallService ?? throw new ArgumentNullException(nameof(hallService));
            _logger = logger.CreateLogger<HallsController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<HallListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId)
        {
            var halls = await _hallService.GetAllAsync(cinemaId);
            return Ok(new ApiResponse<IList<HallListModel>>(halls));
        }

        [HttpGet]
        [Route("{hallId}")]
        [ProducesResponseType(typeof(ApiResponse<HallModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int hallId)
        {
            var hall = await _hallService.GetAsync(cinemaId, hallId);
            return Ok(new ApiResponse<HallModel>(hall));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<HallModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, [FromBody] HallCreateModel hallModel)
        {
            var hall = await _hallService.Create(cinemaId, hallModel);
            return CreatedAtAction(
                nameof(Post),
                new { hallId = hall.Id },
                new ApiResponse<HallModel>(hall)
            );
        }

        [HttpPut]
        [Route("{hallId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int cinemaId, int hallId, [FromBody] HallCreateModel hallModel)
        {
            var hall = await _hallService.Update(cinemaId, hallId, hallModel);
            return Ok(new ApiResponse<HallModel>(hall));
        }

        [HttpDelete]
        [Route("{hallId}")]
        [ProducesResponseType(typeof(ApiResponse<HallModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId, int hallId)
        {
            var hall = await _hallService.Delete(cinemaId, hallId);
            return Ok(new ApiResponse<HallModel>(hall));
        }
    }
}
