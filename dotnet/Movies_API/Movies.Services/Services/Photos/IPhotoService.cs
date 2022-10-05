using Microsoft.AspNetCore.Http;
using Movies.Services.Models.Photos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Photos
{
    public interface IPhotoService
    {
        Task<IList<PhotoModel>> GetAllAsync(int cinemaId);
        Task<IList<PhotoModel>> GetAllAsync(int cinemaId, int movieId);
        Task<IList<PhotoModel>> GetAllAsync(int cinemaId, int movieId, int actorId);
        Task<IList<PhotoModel>> Create(int cinemaId, List<IFormFile> files);
        Task<IList<PhotoModel>> Create(int cinemaId, int movieId, List<IFormFile> files);
        Task<IList<PhotoModel>> Create(int cinemaId, int movieId, int actorId, List<IFormFile> files);
        Task<PhotoModel> Delete(int cinemaId, Guid photoId);
        Task<PhotoModel> Delete(int cinemaId, int movieId, Guid photoId);
        Task<PhotoModel> Delete(int cinemaId, int movieId, int actorId, Guid photoId);
    }
}
