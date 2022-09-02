using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Common;
using Movies.Services.Models.Movies;
using Movies.Services.Services.Movies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movieService, ILoggerFactory logger)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _logger = logger.CreateLogger<MoviesController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<MovieListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieService.GetAllAsync();
            return Ok(new ApiResponse<IList<MovieListModel>>(movies));
        }

        [HttpGet]
        [Route("{movieId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int movieId)
        {
            var movie = await _movieService.GetAsync(movieId);
            return Ok(new ApiResponse<MovieModel>(movie));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MovieModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] MovieCreateModel movieModel)
        {
            var movie = await _movieService.Create(movieModel);
            return CreatedAtAction(
                nameof(Get),
                new { movieId = movie.Id },
                new ApiResponse<MovieModel>(movie)
            );
        }

        [HttpPut]
        [Route("{movieId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int movieId, [FromBody] MovieCreateModel movieModel)
        {
            var movie = await _movieService.Update(movieId, movieModel);
            return Ok(new ApiResponse<MovieModel>(movie));
        }

        [HttpDelete]
        [Route("{movieId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int movieId)
        {
            var movie = await _movieService.Delete(movieId);
            return Ok(new ApiResponse<MovieModel>(movie));
        }
    }
}
