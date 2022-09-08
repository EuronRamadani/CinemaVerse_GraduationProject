using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Movies;
using Movies.Services.Services.Cinemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movies.Services.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(
            IMapper mapper,
            IRepository<Movie> movieRepository,
            IRepository<Cinema> cinemaRepository,
            ILogger<MovieService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _cinemaRepository = cinemaRepository ?? throw new ArgumentNullException(nameof(cinemaRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<MovieModel> GetAsync(int cinemaId, int movieId)
        {
            var movie = await _movieRepository.GetAsync(query => query
                .Where(movie => movie.Id == movieId)
                .Where(movie => movie.Cinema.Id == cinemaId)
                .Select(x => new Movie 
                {
                    Title = x.Title,
                    Description = x.Description,
                    Director = x.Director,
                    TrailerLink = x.TrailerLink,
                    ReleaseYear = x.ReleaseYear,
                    ReleaseDate = x.ReleaseDate,
                    Country = x.Country,
                    Language = x.Language,
                    Genre = x.Genre,
                    Length = x.Length,
                    Deleted = x.Deleted,
                    Cinema = new Cinema { Id = x.Cinema.Id }
                }));

            if (movie == null)
                throw new BaseException($"Movie with id {movieId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var movieModel = _mapper.Map<MovieModel>(movie);

            return movieModel;
        }

        public async Task<IList<MovieListModel>> GetAllAsync(int cinemaId)
        {
            try
            {
                var movies = await _movieRepository.GetAllAsync(query => query
                    .Where(movie => movie.Cinema.Id == cinemaId)
                    .Select(x => new Movie
                    {
                        Title = x.Title,
                        Description = x.Description,
                        Director = x.Director,
                        TrailerLink = x.TrailerLink,
                        ReleaseYear = x.ReleaseYear,
                        ReleaseDate = x.ReleaseDate,
                        Country = x.Country,
                        Language = x.Language,
                        Genre = x.Genre,
                        Length = x.Length,
                        Deleted = x.Deleted,
                        Cinema = new Cinema { Id = x.Cinema.Id }
                    }));

                var moviesList = _mapper.Map<IList<MovieListModel>>(movies);

                return moviesList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all movies",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieModel> Create(int cinemaId, MovieCreateModel movieCreateModel)
        {
            try
            {
                var cinema = await _cinemaRepository.GetAsync(query => query
                    .Where(c => c.Id == cinemaId));

                if(cinema == null)
                    throw new BaseException($"Cinema with id {cinemaId} not found!", ExceptionType.NotFound, HttpStatusCode.NotFound);

                var movie = PrepareMovie(movieCreateModel);
                movie.Cinema = cinema;

                await _movieRepository.InsertAsync(movie);

                var movieModel = _mapper.Map<MovieModel>(movie);

                return movieModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new movie, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create movie", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieModel> Update(int cinemaId, int movieId, MovieCreateModel movieUpdateModel)
        {
            try
            {
                var movie = await _movieRepository.GetAsync(query => query
                    .Where(movie => movie.Id == movieId));

                if (movie == null)
                    throw new BaseException($"Movie with id: {movieId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                _mapper.Map(movieUpdateModel, movie);

                _movieRepository.Update(movie);

                var movieModel = _mapper.Map<MovieModel>(movie);

                return movieModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update movie with id: {movieId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieModel> Delete(int cinemaId, int movieId)
        {
            try
            {
                var movie = await _movieRepository.GetAsync(query => query
                    .Where(movie => movie.Id == movieId));

                _movieRepository.Delete(movie);

                return _mapper.Map<MovieModel>(movie);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update movie with id: {movieId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private Movie PrepareMovie(MovieCreateModel movieCreateModel)
        {
            var movie = _mapper.Map<Movie>(movieCreateModel);
            //add authContextService to get user tracing later!
            movie.InsertDate = DateTime.UtcNow;
            movie.UpdateDate = DateTime.UtcNow;
            return movie;
        }
    }
}
