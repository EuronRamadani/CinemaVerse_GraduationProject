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
    [Route("api/halls")]
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
        public async Task<IActionResult> Get()
        {
            var halls = await _hallService.GetAllAsync();
            return Ok(new ApiResponse<IList<HallListModel>>(halls));
        }

        [HttpGet]
        [Route("{hallId}")]
        [ProducesResponseType(typeof(ApiResponse<HallModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int hallId)
        {
            var hall = await _hallService.GetAsync(hallId);
            return Ok(new ApiResponse<HallModel>(hall));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<HallModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] HallCreateModel hallModel)
        {
            var hall = await _hallService.Create(hallModel);
            return CreatedAtAction(
                nameof(Get),
                new { hallId = hall.Id },
                new ApiResponse<HallModel>(hall)
            );
        }

        [HttpPut]
        [Route("{hallId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int hallId, [FromBody] HallCreateModel hallModel)
        {
            var hall = await _hallService.Update(hallId, hallModel);
            return Ok(new ApiResponse<HallModel>(hall));
        }

        [HttpDelete]
        [Route("{hallId}")]
        [ProducesResponseType(typeof(ApiResponse<HallModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int hallId)
        {
            var hall = await _hallService.Delete(hallId);
            return Ok(new ApiResponse<HallModel>(hall));
        }
    }
}
