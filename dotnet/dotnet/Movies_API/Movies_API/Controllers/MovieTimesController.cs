using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Common;
using Movies.Services.Models.MovieTimes;
using Movies.Services.Services.MovieTimes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/movies/{movieId}/movie-times")]
    public class MovieTimesController : ControllerBase
    {
        private readonly IMovieTimeService _movieTimeService;
        private readonly ILogger<MovieTimesController> _logger;

        public MovieTimesController(IMovieTimeService movieTimeService, ILoggerFactory logger)
        {
            _movieTimeService = movieTimeService ?? throw new ArgumentNullException(nameof(movieTimeService));
            _logger = logger.CreateLogger<MovieTimesController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<MovieTimeModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int movieId)
        {
            var movieTimes = await _movieTimeService.GetAllAsync(cinemaId, movieId);
            return Ok(new ApiResponse<IList<MovieTimeModel>>(movieTimes));
        }

        [HttpGet]
        [Route("{movieTimeId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieTimeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSingle(int cinemaId, int movieTimeId)
        {
            var movieTime = await _movieTimeService.GetAsync(cinemaId, movieTimeId);
            return Ok(new ApiResponse<MovieTimeModel>(movieTime));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MovieTimeModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, int movieId, [FromBody] MovieTimeCreateModel movieTimeCreateModel)
        {
            var movieTime = await _movieTimeService.Create(cinemaId, movieId, movieTimeCreateModel);
            return CreatedAtAction(
                nameof(Post),
                new { movieTimeId = movieTime.Id },
                new ApiResponse<MovieTimeModel>(movieTime)
            );
        }

        [HttpDelete]
        [Route("{movieTimeId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieTimeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId, int movieTimeId)
        {
            var movieTime = await _movieTimeService.Delete(cinemaId, movieTimeId);
            return Ok(new ApiResponse<MovieTimeModel>(movieTime));
        }
    }
}
