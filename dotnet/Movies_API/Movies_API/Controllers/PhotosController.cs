using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Common;
using Movies.Services.Models.Photos;
using Movies.Services.Services.Photos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(IPhotoService photoService, ILoggerFactory logger)
        {
            _photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
            _logger = logger.CreateLogger<PhotosController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("photos")]
        [ProducesResponseType(typeof(ApiResponse<IList<PhotoModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId)
        {
            var cinemaPhotos = await _photoService.GetAllAsync(cinemaId);
            return Ok(new ApiResponse<IList<PhotoModel>>(cinemaPhotos));
        }

        [HttpGet]
        [Route("movies/{movieId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<IList<PhotoModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int movieId)
        {
            var moviePhotos = await _photoService.GetAllAsync(cinemaId, movieId);
            return Ok(new ApiResponse<IList<PhotoModel>>(moviePhotos));
        }

        [HttpGet]
        [Route("movies/{movieId}/actors/{actorId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<IList<PhotoModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int cinemaId, int movieId, int actorId)
        {
            var moviePhotos = await _photoService.GetAllAsync(cinemaId, movieId, actorId);
            return Ok(new ApiResponse<IList<PhotoModel>>(moviePhotos));
        }

        [HttpPost]
        [Route("photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, List<IFormFile> files)
        {
            var cinemaPhotos = await _photoService.Create(cinemaId, files);
            return CreatedAtAction(
                nameof(Post),
                new ApiResponse<IList<PhotoModel>>(cinemaPhotos)
            );
        }        
        
        [HttpPost]
        [Route("movies/{movieId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, int movieId, List<IFormFile> files)
        {
            var cinemaPhotos = await _photoService.Create(cinemaId, movieId, files);
            return CreatedAtAction(
                nameof(Post),
                new ApiResponse<IList<PhotoModel>>(cinemaPhotos)
            );
        }

        [HttpPost]
        [Route("movies/{movieId}/actors/{actorId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(int cinemaId, int movieId, int actorId, List<IFormFile> files)
        {
            var cinemaPhotos = await _photoService.Create(cinemaId, movieId, actorId, files);
            return CreatedAtAction(
                nameof(Post),
                new ApiResponse<IList<PhotoModel>>(cinemaPhotos)
            );
        }

        [HttpDelete]
        [Route("photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId, Guid photoId)
        {
            var photo = await _photoService.Delete(cinemaId, photoId);
            return Ok(new ApiResponse<PhotoModel>(photo));
        }

        [HttpDelete]
        [Route("movies/{movieId}/photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId, int movieId, Guid photoId)
        {
            var photo = await _photoService.Delete(cinemaId, movieId, photoId);
            return Ok(new ApiResponse<PhotoModel>(photo));
        }

        [HttpDelete]
        [Route("movies/{movieId}/actors/{actorId}/photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cinemaId, int movieId, int actorId, Guid photoId)
        {
            var photo = await _photoService.Delete(cinemaId, movieId, actorId, photoId);
            return Ok(new ApiResponse<PhotoModel>(photo));
        }
    }
}
