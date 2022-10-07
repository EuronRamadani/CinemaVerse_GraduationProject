using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Services.Services.Reviews;
using Movies.Services.Models.Reviews;

namespace Movies.API.Controllers
{
    [Route("api/movies/{movieId}/reviews")]
    public class MovieReviewController : ControllerBase
    {
        private readonly IMovieReviewService _movieReviewService;
        private readonly ILogger<MovieReviewController> _logger;

        public MovieReviewController(IMovieReviewService MovieReviewService, ILoggerFactory logger)
        {
            _movieReviewService = MovieReviewService ?? throw new ArgumentNullException(nameof(MovieReviewService));
            _logger = logger.CreateLogger<MovieReviewController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<MovieReviewListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int movieId)
        {
            var Reviews = await _movieReviewService.GetAllAsync(movieId);
            return Ok(new ApiResponse<IList<MovieReviewListModel>>(Reviews));
        }

        [HttpGet]
        [Route("{ReviewId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieReviewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int movieId, int ReviewId)
        {
            var Review = await _movieReviewService.GetAsync(movieId, ReviewId);
            return Ok(new ApiResponse<MovieReviewModel>(Review));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MovieReviewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int movieId, [FromBody] MovieReviewCreateModel ReviewModel)
        {
            var Review = await _movieReviewService.Create(movieId, ReviewModel);
            return CreatedAtAction(
                nameof(Post),
                new { ReviewId = Review.Id },
                new ApiResponse<MovieReviewModel>(Review)
            );
        }

        [HttpPut]
        [Route("{ReviewId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int ReviewId, [FromBody] MovieReviewCreateModel ReviewModel)
        {
            var Review = await _movieReviewService.Update(ReviewId, ReviewModel);
            return Ok(new ApiResponse<MovieReviewModel>(Review));
        }

        [HttpDelete]
        [Route("{ReviewId}")]
        [ProducesResponseType(typeof(ApiResponse<MovieReviewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int ReviewId)
        {
            var Review = await _movieReviewService.Delete(ReviewId);
            return Ok(new ApiResponse<MovieReviewModel>(Review));
        }
    }
}
