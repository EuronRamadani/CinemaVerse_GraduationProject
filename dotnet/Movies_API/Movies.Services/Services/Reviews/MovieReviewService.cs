using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Events;
using Movies.Services.Models.Reviews;
using Movies.Services.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Reviews
{
    public class MovieReviewService : IMovieReviewService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MovieReview> _movieReviewRepository;
        private readonly IRepository<Movie> _cinemaRepository;
        private readonly ILogger<MovieReviewService> _logger;

        public MovieReviewService(
          IMapper mapper,
          IRepository<MovieReview> MovieReviewRepository,
          ILogger<MovieReviewService> logger,
          IRepository<Movie> movieRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieReviewRepository = MovieReviewRepository ?? throw new ArgumentNullException(nameof(MovieReviewRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cinemaRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public async Task<MovieReviewModel> Create(int movieId, MovieReviewCreateModel movieReviewCreateModel)
        {
            try
            {
                var movie = await _cinemaRepository.GetAsync(query => query
                    .Where(m => m.Id == movieId));

                if (movie == null)
                    throw new BaseException($"Movie with id {movieId} not found!", ExceptionType.NotFound, HttpStatusCode.NotFound);

                var movieEvent = PrepareMovieReview(movieReviewCreateModel);

                movieEvent.Movie = movie;
                movieEvent.MovieId = movieId;
                movieEvent.ReviewDate=DateTime.Now;

                await _movieReviewRepository.InsertAsync(movieEvent);

                var movieReviewModel = _mapper.Map<MovieReviewModel>(movieEvent);

                return movieReviewModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new review, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create review", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieReviewModel> Delete(int ReviewId)
        {
            try
            {
                var MovieReview = await _movieReviewRepository.GetAsync(query => query
                    .Where(MovieReview => MovieReview.Id == ReviewId));

                _movieReviewRepository.Delete(MovieReview);

                return _mapper.Map<MovieReviewModel>(MovieReview);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete review with id: {ReviewId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IList<MovieReviewListModel>> GetAllAsync(int movieId)
        {
            try
            {
                var reviews = await _movieReviewRepository.GetAllAsync(query => query
                    .Where(reviews => reviews.Movie.Id == movieId));

                var reviewList = _mapper.Map<IList<MovieReviewListModel>>(reviews);

                return reviewList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all reviews",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieReviewModel> GetAsync(int movieId, int ReviewId)
        {
            var reviews = await _movieReviewRepository.GetAsync(query => query
                .Where(reviews => reviews.Id == ReviewId)
                .Where(movies => movies.MovieId == movieId));

            if (reviews == null)
                throw new BaseException($"Review with id {ReviewId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var reviewsModel = _mapper.Map<MovieReviewModel>(reviews);

            return reviewsModel;
        }

        public async Task<MovieReviewModel> Update(int ReviewId, MovieReviewCreateModel MovieReviewUpdateModel)
        {
            try
            {
                var Review = await _movieReviewRepository.GetAsync(query => query
                    .Where(Review => Review.Id == ReviewId));

                if (Review == null)
                    throw new BaseException($"Review with id: {Review} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                _mapper.Map(MovieReviewUpdateModel, Review);

                _movieReviewRepository.Update(Review);

                var ReviewModel = _mapper.Map<MovieReviewModel>(Review);

                return ReviewModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update review with id: {ReviewId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private MovieReview PrepareMovieReview(MovieReviewCreateModel MovieReviewCreateModel)
        {
            var MovieReview = _mapper.Map<MovieReview>(MovieReviewCreateModel);
            MovieReview.InsertDate = DateTime.UtcNow;
            MovieReview.UpdateDate = DateTime.UtcNow;
            return MovieReview;
        }
    }
}
