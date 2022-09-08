using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Cinemas;
using Movies.Services.Models.Common;
using Movies.Services.Services.Cinemas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;
        private readonly ILogger<CinemasController> _logger;

        public CinemasController(ICinemaService cinemaService, ILoggerFactory logger)
        {
            _cinemaService = cinemaService ?? throw new ArgumentNullException(nameof(cinemaService));
            _logger = logger.CreateLogger<CinemasController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<CinemaListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var cinemas = await _cinemaService.GetAllAsync();
            return Ok(new ApiResponse<IList<CinemaListModel>>(cinemas));
        }

        [HttpGet]
        [Route("{cinemaId}")]
        [ProducesResponseType(typeof(ApiResponse<CinemaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId)
        {
            var cinema = await _cinemaService.GetAsync(cinemaId);
            return Ok(new ApiResponse<CinemaModel>(cinema));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CinemaModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CinemaCreateModel cinemaModel)
        {
            var cinema = await _cinemaService.Create(cinemaModel);
            return CreatedAtAction(
                nameof(Get),
                new { cinemaId = cinema.Id },
                new ApiResponse<CinemaModel>(cinema)
            );
        }

        [HttpPut]
        [Route("{cinemaId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int cinemaId, [FromBody] CinemaCreateModel cinemaModel)
        {
            var cinema = await _cinemaService.Update(cinemaId, cinemaModel);
            return Ok(new ApiResponse<CinemaModel>(cinema));
        }

        [HttpDelete]
        [Route("{cinemaId}")]
        [ProducesResponseType(typeof(ApiResponse<CinemaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId)
        {
            var cinema = await _cinemaService.Delete(cinemaId);
            return Ok(new ApiResponse<CinemaModel>(cinema));
        }
    }
}
