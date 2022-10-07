using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Actors;
using Movies.Services.Models.Common;
using Movies.Services.Models.Movies;
using Movies.Services.Services.Movies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/movies")]
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
        public async Task<IActionResult> Get(int cinemaId)
        {
            var movies = await _movieService.GetAllAsync(cinemaId);
            return Ok(new ApiResponse<IList<MovieListModel>>(movies));
        }

        [HttpGet]
        [Route("{movieId}/actors")]
        [ProducesResponseType(typeof(ApiResponse<IList<ActorListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActors(int movieId)
        {
            var actors = await _movieService.GetAllActorsAsync(movieId);
            return Ok(new ApiResponse<IList<ActorListModel>>(actors));
        }

        [HttpGet]
        [Route("{movieId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int movieId)
        {
            var movie = await _movieService.GetAsync(cinemaId, movieId);
            return Ok(new ApiResponse<MovieModel>(movie));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MovieModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, [FromBody] MovieCreateModel movieModel)
        {
            var movie = await _movieService.Create(cinemaId, movieModel);
            return CreatedAtAction(
                nameof(Post),
                new { movieId = movie.Id },
                new ApiResponse<MovieModel>(movie)
            );
        }

        [HttpPut]
        [Route("{movieId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int cinemaId, int movieId, [FromBody] MovieCreateModel movieModel)
        {
            var movie = await _movieService.Update(cinemaId, movieId, movieModel);
            return Ok(new ApiResponse<MovieModel>(movie));
        }

        [HttpDelete]
        [Route("{movieId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId, int movieId)
        {
            var movie = await _movieService.Delete(cinemaId, movieId);
            return Ok(new ApiResponse<MovieModel>(movie));
        }
    }
}
