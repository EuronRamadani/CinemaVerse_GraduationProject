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
        public async Task<IActionResult> GetAllCinemaPhotosAsync(int cinemaId)
        {
            var cinemaPhotos = await _photoService.GetAllCinemaPhotosAsync(cinemaId);
            return Ok(new ApiResponse<IList<PhotoModel>>(cinemaPhotos));
        }

        [HttpGet]
        [Route("movies/{movieId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<IList<PhotoModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMoviePhotosAsync(int cinemaId, int movieId)
        {
            var moviePhotos = await _photoService.GetAllMoviePhotosAsync(cinemaId, movieId);
            return Ok(new ApiResponse<IList<PhotoModel>>(moviePhotos));
        }

        [HttpGet]
        [Route("events/{eventId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<IList<PhotoModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEventPhotosAsync(int cinemaId, int eventId)
        {
            var eventsPhotos = await _photoService.GetAllEventPhotosAsync(cinemaId, eventId);
            return Ok(new ApiResponse<IList<PhotoModel>>(eventsPhotos));
        }

        [HttpGet]
        [Route("actors/{actorId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<IList<PhotoModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllActorPhotosAsync(int cinemaId, int actorId)
        {
            var actorPhotos = await _photoService.GetAllActorPhotosAsync(cinemaId, actorId);
            return Ok(new ApiResponse<IList<PhotoModel>>(actorPhotos));
        }


        [HttpPost]
        [Route("photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCinemaPhoto(int cinemaId, List<IFormFile> files)
        {
            var cinemaPhotos = await _photoService.CreateCinemaPhoto(cinemaId, files);
            return CreatedAtAction(
                nameof(CreateCinemaPhoto),
                new ApiResponse<IList<PhotoModel>>(cinemaPhotos)
            );
        }

        [HttpPost]
        [Route("movies/{movieId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMoviePhoto(int cinemaId, int movieId, List<IFormFile> files)
        {
            var moviePhotos = await _photoService.CreateMoviePhoto(cinemaId, movieId, files);
            return CreatedAtAction(
                nameof(CreateEventPhoto),
                new ApiResponse<IList<PhotoModel>>(moviePhotos)
            );
        }

        [HttpPost]
        [Route("events/{eventId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEventPhoto(int cinemaId, int eventId, List<IFormFile> files)
        {
            var eventPhotos = await _photoService.CreateEventPhoto(cinemaId, eventId, files);
            return CreatedAtAction(
                nameof(CreateEventPhoto),
                new ApiResponse<IList<PhotoModel>>(eventPhotos)
            );
        }

        [HttpPost]
        [Route("actors/{actorId}/photos")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateActorPhoto(int cinemaId, int actorId, List<IFormFile> files)
        {
            var actorPhotos = await _photoService.CreateActorPhoto(cinemaId, actorId, files);
            return CreatedAtAction(
                nameof(CreateActorPhoto),
                new ApiResponse<IList<PhotoModel>>(actorPhotos)
            );
        }

        [HttpDelete]
        [Route("photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCinemaPhoto(int cinemaId, Guid photoId)
        {
            var cinemaPhoto = await _photoService.DeleteCinemaPhoto(cinemaId, photoId);
            return Ok(new ApiResponse<PhotoModel>(cinemaPhoto));
        }

        [HttpDelete]
        [Route("movies/{movieId}/photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMoviePhoto(int cinemaId, int movieId, Guid photoId)
        {
            var moviePhoto = await _photoService.DeleteMoviePhoto(cinemaId, movieId, photoId);
            return Ok(new ApiResponse<PhotoModel>(moviePhoto));
        }

        [HttpDelete]
        [Route("events/{eventId}/photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventPhoto(int cinemaId, int eventId, Guid photoId)
        {
            var eventPhoto = await _photoService.DeleteEventPhoto(cinemaId, eventId, photoId);
            return Ok(new ApiResponse<PhotoModel>(eventPhoto));
        }

        [HttpDelete]
        [Route("actors/{actorId}/photos/{photoId}")]
        [ProducesResponseType(typeof(ApiResponse<PhotoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActorPhoto(int cinemaId, int actorId, Guid photoId)
        {
            var actorPhoto = await _photoService.DeleteActorPhoto(cinemaId, actorId, photoId);
            return Ok(new ApiResponse<PhotoModel>(actorPhoto));
        }
    }
}
