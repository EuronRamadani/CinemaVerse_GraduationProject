using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Movies;
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
        private readonly ILogger<MovieService> _logger;

        public MovieService(
            IMapper mapper,
            IRepository<Movie> movieRepository,
            ILogger<MovieService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<MovieListModel>> GetAllAsync()
        {
            try
            {
                var movies = await _movieRepository.GetAllAsync(query => query);

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

        public async Task<MovieModel> GetAsync(int movieId)
        {
            var movie = await _movieRepository.GetAsync(query => query
                .Where(movie => movie.Id == movieId));

            if (movie == null)
                throw new BaseException($"Movie with id {movieId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var movieModel = _mapper.Map<MovieModel>(movie);

            return movieModel;
        }

        public async Task<MovieModel> Create(MovieCreateModel movieCreateModel)
        {
            try
            {
                var movie = PrepareMovie(movieCreateModel);

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

        public async Task<MovieModel> Update(int movieId, MovieCreateModel movieUpdateModel)
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

        public async Task<MovieModel> Delete(int movieId)
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
