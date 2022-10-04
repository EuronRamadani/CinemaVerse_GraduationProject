using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Photos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movies.Services.Services.Photos
{
    public class PhotoService : IPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly ILogger<PhotoService> _logger;

        public PhotoService(
            IMapper mapper,
            IRepository<Photo> photoRepository,
            IRepository<Movie> movieRepository,
            IRepository<Actor> actorRepository,
            IRepository<Event> eventRepository,
            IRepository<Cinema> cinemaRepository,
            ILogger<PhotoService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _cinemaRepository = cinemaRepository ?? throw new ArgumentNullException(nameof(cinemaRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<PhotoModel>> GetAllAsync(int cinemaId)
        {
            var cinema = await _cinemaRepository.GetAsync(query => query
                .Where(cinema => cinema.Id == cinemaId)
                .Include(cinema => cinema.Photos
                    .Where(x => x.Deleted == false)));

            if (cinema == null)
                throw new BaseException($"Cinema with id: {cinemaId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photos = cinema.Photos;

            var photosModel = _mapper.Map<List<PhotoModel>>(photos);

            return photosModel;
        }

        public async Task<IList<PhotoModel>> GetAllAsync(int cinemaId, int movieId)
        {
            var movie = await _movieRepository.GetAsync(query => query
                .Where(movie => movie.CinemaId == cinemaId)
                .Where(movie => movie.Id == movieId)
                .Include(movie => movie.Photos
                        .Where(x => x.Deleted == false)));

            if (movie == null)
                throw new BaseException($"Movie with id: {movieId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photos = movie.Photos;

            var photosModel = _mapper.Map<List<PhotoModel>>(photos);

            return photosModel;
        }

        public async Task<IList<PhotoModel>> GetAllAsyncc(int cinemaId, int eventId)
        {
            var eventt = await _eventRepository.GetAsync(query => query
                .Where(eventt => eventt.CinemaId == cinemaId)
                .Where(eventt => eventt.Id == eventId)
                .Include(eventt => eventt.Photos
                        .Where(x => x.Deleted == false)));

            if (eventt == null)
                throw new BaseException($"Actor with id: {eventId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photos = eventt.Photos;

            var photosModel = _mapper.Map<List<PhotoModel>>(photos);

            return photosModel;
        }

        public async Task<IList<PhotoModel>> GetAllAsync(int cinemaId, int movieId, int actorId)
        {
            var actor = await _actorRepository.GetAsync(query => query
                //.Where(actor => actor.CinemaId == cinemaId)
                .Where(actor => actor.Id == movieId)
                .Where(actor => actor.Id == actorId)
                .Include(movie => movie.Photos
                        .Where(x => x.Deleted == false)));

            if (actor == null)
                throw new BaseException($"Actor with id: {actorId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photos = actor.Photos;

            var photosModel = _mapper.Map<List<PhotoModel>>(photos);

            return photosModel;
        }

        

        public async Task<IList<PhotoModel>> Create(int cinemaId, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var client_assets_folder = Directory.GetDirectories(@"../../../client-side/public/assets")[0];
            
            string path = client_assets_folder + "/Cinemas";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var photosToReturn = new List<PhotoModel>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    FileInfo fileInfo = new FileInfo(formFile.FileName);
                    string fileName = formFile.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    PhotoModel photoModel = await PrepareCinemaPhoto(cinemaId, fileName, fileNameWithPath);

                    photosToReturn.Add(photoModel);
                }
            }

            return photosToReturn;
        }

        public async Task<IList<PhotoModel>> Create(int cinemaId, int movieId, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var client_assets_folder = Directory.GetDirectories(@"../../../client-side/public/assets")[0];
            
            string path = client_assets_folder + "/Movies";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var photosToReturn = new List<PhotoModel>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    FileInfo fileInfo = new FileInfo(formFile.FileName);
                    string fileName = formFile.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    PhotoModel photoModel = await PrepareMoviePhoto(cinemaId, movieId, fileName, fileNameWithPath);

                    photosToReturn.Add(photoModel);
                }
            }

            return photosToReturn;
        }

        public async Task<IList<PhotoModel>> Createe(int cinemaId, int eventId, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var client_assets_folder = Directory.GetDirectories(@"../../../client-side/public/assets")[0];

            string path = client_assets_folder + "/Events";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var photosToReturn = new List<PhotoModel>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    FileInfo fileInfo = new FileInfo(formFile.FileName);
                    string fileName = formFile.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    PhotoModel photoModel = await PrepareEventPhoto(cinemaId, eventId, fileName, fileNameWithPath);

                    photosToReturn.Add(photoModel);
                }
            }

            return photosToReturn;
        }

        public async Task<IList<PhotoModel>> Create(int cinemaId, int movieId, int actorId, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var client_assets_folder = Directory.GetDirectories(@"../../../client-side/public/assets")[0];

            string path = client_assets_folder + "/Actors";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var photosToReturn = new List<PhotoModel>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    FileInfo fileInfo = new FileInfo(formFile.FileName);
                    string fileName = formFile.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    PhotoModel photoModel = await PrepareActorPhoto(cinemaId, movieId, actorId, fileName, fileNameWithPath);

                    photosToReturn.Add(photoModel);
                }
            }

            return photosToReturn;
        }

        

        public async Task<PhotoModel> Delete(int cinemaId, Guid photoId)
        {
            var cinema = await _cinemaRepository.GetAsync(query => query
                .Where(cinema => cinema.Id == cinemaId)
                .Include(cinema => cinema.Photos));

            if (cinema == null)
                throw new BaseException($"Cinema with id: {cinemaId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photo = cinema.Photos
                .Where(photo => photo.LongId.Equals(photoId)).FirstOrDefault();

            var photoModel = _mapper.Map<PhotoModel>(photo);

            _photoRepository.Delete(photo);

            return photoModel;
        }

        public async Task<PhotoModel> Delete(int cinemaId, int movieId, Guid photoId)
        {
            var movie = await _movieRepository.GetAsync(query => query
                .Where(movie => movie.CinemaId == cinemaId)
                .Where(movie => movie.Id == movieId)
                .Include(cinema => cinema.Photos));

            if (movie == null)
                throw new BaseException($"Movie with id: {cinemaId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photo = movie.Photos
                .Where(photo => photo.LongId.Equals(photoId)).FirstOrDefault();

            var photoModel = _mapper.Map<PhotoModel>(photo);

            _photoRepository.Delete(photo);

            return photoModel;
        }

        public async Task<PhotoModel> Deletee(int cinemaId, int eventId, Guid photoId)
        {
            var eventt = await _eventRepository.GetAsync(query => query
                .Where(eventt => eventt.CinemaId == cinemaId)
                .Where(eventt => eventt.Id == eventId)
                .Include(cinema => cinema.Photos));

            if (eventt == null)
                throw new BaseException($"Event with id: {eventId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photo = eventt.Photos
                .Where(photo => photo.LongId.Equals(photoId)).FirstOrDefault();

            var photoModel = _mapper.Map<PhotoModel>(photo);

            _photoRepository.Delete(photo);

            return photoModel;
        }

        public async Task<PhotoModel> Delete(int cinemaId, int movieId, int actorId, Guid photoId)
        {
            var actor = await _actorRepository.GetAsync(query => query
                //.Where(movie => movie.CinemaId == cinemaId)
                .Where(actor => actor.Id == movieId)
                .Where(actor => actor.Id == actorId)
                .Include(cinema => cinema.Photos));

            if (actor == null)
                throw new BaseException($"Actor with id: {actorId} does not exist",
                    ExceptionType.NotFound, HttpStatusCode.NotFound);

            var photo = actor.Photos
                .Where(photo => photo.LongId.Equals(photoId)).FirstOrDefault();

            var photoModel = _mapper.Map<PhotoModel>(photo);

            _photoRepository.Delete(photo);

            return photoModel;
        }

       

        private async Task<PhotoModel> PrepareCinemaPhoto(int cinemaId, string fileName, string fileNameWithPath)
        {
            var photo = new Photo()
            {
                LongId = Guid.NewGuid(),
                Name = fileName,
                Description = null,
                ImgPath = fileNameWithPath,
                ImgClientPath = "http://localhost:8080/assets/app_files/Cinemas/" + fileName,
                Deleted = false,
                InsertDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await InsertPhotoToCinema(cinemaId, photo);

            var photoModel = _mapper.Map<PhotoModel>(photo);

            return photoModel;
        }

        private async Task<PhotoModel> PrepareMoviePhoto(int cinemaId, int movieId, string fileName, string fileNameWithPath)
        {
            var photo = new Photo()
            {
                LongId = Guid.NewGuid(),
                Name = fileName,
                Description = null,
                ImgPath = fileNameWithPath,
                ImgClientPath = "http://localhost:8080/assets/app_files/Movies/" + fileName,
                Deleted = false,
                InsertDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await InsertPhotoToMovie(cinemaId, movieId, photo);

            var photoModel = _mapper.Map<PhotoModel>(photo);

            return photoModel;
        }
        private async Task<PhotoModel> PrepareActorPhoto(int cinemaId, int movieId, int actorId, string fileName, string fileNameWithPath)
        {
            var photo = new Photo()
            {
                LongId = Guid.NewGuid(),
                Name = fileName,
                Description = null,
                ImgPath = fileNameWithPath,
                ImgClientPath = "http://localhost:8080/assets/app_files/Actors/" + fileName,
                Deleted = false,
                InsertDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await InsertPhotoToActor(cinemaId, movieId, actorId, photo);

            var photoModel = _mapper.Map<PhotoModel>(photo);

            return photoModel;
        }

        private async Task<PhotoModel> PrepareEventPhoto(int cinemaId, int eventId, string fileName, string fileNameWithPath)
        {
            var photo = new Photo()
            {
                LongId = Guid.NewGuid(),
                Name = fileName,
                Description = null,
                ImgPath = fileNameWithPath,
                ImgClientPath = "http://localhost:8080/assets/app_files/Events/" + fileName,
                Deleted = false,
                InsertDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await InsertPhotoToEvent(cinemaId, eventId, photo);

            var photoModel = _mapper.Map<PhotoModel>(photo);

            return photoModel;
        }

        private async Task InsertPhotoToCinema(int cinemaId, Photo photo)
        {
            try
            {
                var cinema = await _cinemaRepository.GetAsync(query => query
                    .Where(cinema => cinema.Id == cinemaId)
                    .Include(cinema => cinema.Photos));

                if (cinema == null)
                    throw new BaseException($"Cinema with id: {cinemaId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                cinema.Photos.Add(photo);
                cinema.UpdateDate = DateTime.UtcNow;

                _cinemaRepository.Update(cinema);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new photo to cinema, with exception message: {Message}", ex.Message);

                if (ex is BaseException) throw;
                throw new BaseException($"Failed to add photo", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        private async Task InsertPhotoToMovie(int cinemaId, int movieId, Photo photo)
        {
            try
            {
                var movie = await _movieRepository.GetAsync(query => query
                    .Where(movie => movie.CinemaId== cinemaId)
                    .Where(movie => movie.Id == movieId)
                    .Include(cinema => cinema.Photos));

                if (movie == null)
                    throw new BaseException($"Movie with id: {cinemaId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                movie.Photos.Add(photo);
                movie.UpdateDate = DateTime.UtcNow;

                _movieRepository.Update(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new photo to movie, with exception message: {Message}", ex.Message);

                if (ex is BaseException) throw;
                throw new BaseException($"Failed to add photo", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        private async Task InsertPhotoToEvent(int cinemaId, int eventId, Photo photo)
        {
            try
            {
                var eventt = await _eventRepository.GetAsync(query => query
                    .Where(eventt => eventt.CinemaId == cinemaId)
                    .Where(eventt => eventt.Id == eventId)
                    .Include(cinema => cinema.Photos));

                if (eventt == null)
                    throw new BaseException($"Actor with id: {eventId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                eventt.Photos.Add(photo);
                eventt.UpdateDate = DateTime.UtcNow;

                _eventRepository.Update(eventt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new photo to movie, with exception message: {Message}", ex.Message);

                if (ex is BaseException) throw;
                throw new BaseException($"Failed to add photo", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        private async Task InsertPhotoToActor(int cinemaId, int movieId, int actorId, Photo photo)
        {
            try
            {
                var actor = await _actorRepository.GetAsync(query => query
                    //.Where(actor => actor. == cinemaId)
                    .Where(actor => actor.Id == movieId)
                    .Where(actor => actor.Id == actorId)
                    .Include(cinema => cinema.Photos));

                if (actor == null)
                    throw new BaseException($"Actor with id: {actorId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                actor.Photos.Add(photo);
                actor.UpdateDate = DateTime.UtcNow;

                _actorRepository.Update(actor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new photo to movie, with exception message: {Message}", ex.Message);

                if (ex is BaseException) throw;
                throw new BaseException($"Failed to add photo", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

       

    }
}
