using Microsoft.AspNetCore.Http;
using Movies.Services.Models.Photos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Photos
{
    public interface IPhotoService
    {
        Task<IList<PhotoModel>> GetAllCinemaPhotosAsync(int cinemaId);
        Task<IList<PhotoModel>> GetAllMoviePhotosAsync(int cinemaId, int movieId);
        Task<IList<PhotoModel>> GetAllActorPhotosAsync(int cinemaId, int actorId);
        Task<IList<PhotoModel>> GetAllEventPhotosAsync(int cinemaId, int eventId);
        Task<IList<PhotoModel>> CreateCinemaPhoto(int cinemaId, List<IFormFile> files);
        Task<IList<PhotoModel>> CreateMoviePhoto(int cinemaId, int movieId, List<IFormFile> files);
        Task<IList<PhotoModel>> CreateActorPhoto(int cinemaId, int actorId, List<IFormFile> files);
        Task<IList<PhotoModel>> CreateEventPhoto(int cinemaId, int eventId, List<IFormFile> files);
        Task<PhotoModel> DeleteCinemaPhoto(int cinemaId, Guid photoId);
        Task<PhotoModel> DeleteMoviePhoto(int cinemaId, int movieId, Guid photoId);
        Task<PhotoModel> DeleteActorPhoto(int cinemaId, int actorId, Guid photoId);
        Task<PhotoModel> DeleteEventPhoto(int cinemaId, int eventId, Guid photoId);
    }
}
