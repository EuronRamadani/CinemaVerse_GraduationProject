using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Common;
using Movies.Services.Models.MovieTimes;
using Movies.Services.Services.Cinemas;
using Movies.Services.Services.Halls;
using Movies.Services.Services.Movies;
using Movies.Services.Services.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Movies.Services.Services.MovieTimes
{
    public class MovieTimeService : IMovieTimeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MovieTime> _movieTimeRepository;
        private readonly IRepository<Hall> _hallRepository;
        private readonly IRepository<Movie> _movieRepository;
        private readonly ICinemaService _cinemaService;
        private readonly ITicketService _ticketService;
        private readonly ILogger<MovieTimeService> _logger;

        public MovieTimeService(
            IMapper mapper,
            IRepository<MovieTime> movieTimeRepository,
            IRepository<Hall> hallRepository,
            IRepository<Movie> movieRepository,
            ICinemaService cinemaService,
            ITicketService ticketService,
            ILogger<MovieTimeService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _movieTimeRepository = movieTimeRepository ?? throw new ArgumentNullException(nameof(movieTimeRepository));
            _hallRepository = hallRepository ?? throw new ArgumentNullException(nameof(hallRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _cinemaService = cinemaService ?? throw new ArgumentNullException(nameof(cinemaService));
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<MovieTimeModel>> GetAllAsync(int cinemaId, int movieId)
        {
            try
            {
                var movieTimes = await _movieTimeRepository.GetAllAsync(query => query
                    .Include(movieTime => movieTime.Movie)
                    .Where(movieTime => movieTime.Movie.CinemaId == cinemaId)
                    .Where(movieTime => movieTime.Movie.Id == movieId)
                    .OrderBy(movieTime => movieTime.StartTime));

                var movieTimesList = _mapper.Map<IList<MovieTimeModel>>(movieTimes);

                return movieTimesList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all movie times",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieTimeModel> GetAsync(int cinemaId, int movieTimeId)
        {
            var movieTime = await _movieTimeRepository.GetAsync(query => query
                .Where(movieTime => movieTime.Movie.CinemaId == cinemaId)
                .Where(movieTime => movieTime.Id == movieTimeId));

            if (movieTime == null)
                throw new BaseException($"Movie Time with id {movieTime} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var movieTimeModel = _mapper.Map<MovieTimeModel>(movieTime);

            return movieTimeModel;
        }

        public async Task<MovieTimeModel> Create(int cinemaId, int movieId, MovieTimeCreateModel movieTimeCreateModel)
        {
            try
            {
                var timeRange = new TimeRange
                {
                    StartTime = movieTimeCreateModel.StartTime,
                    EndTime = movieTimeCreateModel.EndTime,
                };

                var length = timeRange.EndTime - timeRange.StartTime;

                if (length.TotalMinutes < 30)
                    throw new BaseException($"Cannot reserve hall for less than 30 minutes!", ExceptionType.ServerError,
                        HttpStatusCode.BadRequest);

                var currentTime = DateTime.Now;

                if (timeRange.StartTime < currentTime || timeRange.EndTime < currentTime)
                    throw new BaseException($"Cannot set past time in movie times!", ExceptionType.ServerError,
                        HttpStatusCode.BadRequest);

                if (timeRange.StartTime > timeRange.EndTime)
                    throw new BaseException($"End time cannot be earlier than start time!", ExceptionType.ServerError,
                        HttpStatusCode.BadRequest);

                var cinemaModel = await _cinemaService.GetAsync(cinemaId);

                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.CinemaId == cinemaId)
                    .Where(hall => hall.Id == movieTimeCreateModel.HallId)
                    .Include(hall => hall.Rows)
                        .ThenInclude(row => row.Seats)
                    .AsNoTracking());

                if (hall == null)
                    throw new BaseException($"Hall with id {movieTimeCreateModel.HallId} not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var movie = await _movieRepository.GetAsync(query => query
                    .Where(movie => movie.CinemaId == cinemaId)
                    .Where(movie => movie.Id == movieId)
                    .AsNoTracking());

                if (movie == null)
                    throw new BaseException($"Movie with id {movieId} not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var isHallAvailable = await IsHallAvailable(cinemaId, movieTimeCreateModel.HallId, timeRange);

                if (!isHallAvailable)
                    throw new BaseException($"Hall with id {movieTimeCreateModel.HallId} is not available at specified time!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var movieTime = PrepareMovieTime(movieTimeCreateModel);
                movieTime.MovieId = movieId;

                await _movieTimeRepository.InsertAsync(movieTime);

                var movieTimeModel = _mapper.Map<MovieTimeModel>(movieTime);

                await CreateTickets(cinemaId, hall.Id, movieTimeModel.Id, hall.Rows);

                return movieTimeModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new movie time, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create movie time", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<MovieTimeModel> Delete(int cinemaId, int movieTimeId)
        {
            try
            {
                var movieTimeModel = await GetAsync(cinemaId, movieTimeId);

                var movieTime = _mapper.Map<MovieTime>(movieTimeModel);

                _movieTimeRepository.Delete(movieTime);

                return movieTimeModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete movie time with id: {movieTimeId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private async Task<bool> IsHallAvailable(int cinemaId, int hallId, TimeRange timeRange)
        {
            try
            {
                var movieTimes = await _movieTimeRepository.GetAllAsync(query => query
                    .Include(movieTime => movieTime.Movie)
                    .Where(movieTime => movieTime.Movie.CinemaId == cinemaId)
                    .Where(movieTime => movieTime.HallId == hallId));

                if (movieTimes == null) return true;

                foreach (var movieTime in movieTimes)
                {
                    var isEarlier = movieTime.StartTime > timeRange.EndTime;
                    var isLater = movieTime.EndTime < timeRange.StartTime;

                    if (!isEarlier && !isLater) return false;
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to check for hall availabilty!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<Ticket>> CreateTickets(int cinemaId, int hallId, int movieTimeId, List<Row> rows)
        {
            var seatIds = new List<int> { };

            foreach (var row in rows)
            {
                foreach (var seat in row.Seats)
                {
                    seatIds.Add(seat.Id);
                }
            }

            var ticketList = await _ticketService.Create(cinemaId, hallId, movieTimeId, seatIds);

            var ticketListModel = _mapper.Map<List<Ticket>>(ticketList);

            return ticketListModel;
        }

        private MovieTime PrepareMovieTime(MovieTimeCreateModel movieTimeCreateModel)
        {
            var movieTime = _mapper.Map<MovieTime>(movieTimeCreateModel);
            //add authContextService to get user tracing later!
            movieTime.InsertDate = DateTime.UtcNow;
            movieTime.UpdateDate = DateTime.UtcNow;
            return movieTime;
        }
    }
}
