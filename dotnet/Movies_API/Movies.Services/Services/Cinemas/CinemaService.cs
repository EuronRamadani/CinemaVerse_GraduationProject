using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Cinemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Cinemas
{
    public class CinemaService : ICinemaService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly ILogger<CinemaService> _logger;

        public CinemaService(
            IMapper mapper,
            IRepository<Cinema> cinemaRepository,
            ILogger<CinemaService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cinemaRepository = cinemaRepository ?? throw new ArgumentNullException(nameof(cinemaRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CinemaModel> GetAsync(int cinemaId)
        {
            var cinema = await _cinemaRepository.GetAsync(query => query
                .Where(cinema => cinema.Id == cinemaId));

            if (cinema == null)
                throw new BaseException($"Cinema with id {cinemaId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var cinemaModel = _mapper.Map<CinemaModel>(cinema);

            return cinemaModel;
        }

        public async Task<IList<CinemaListModel>> GetAllAsync()
        {
            try
            {
                var cinemas = await _cinemaRepository.GetAllAsync(query => query);

                var cinemasList = _mapper.Map<IList<CinemaListModel>>(cinemas);

                return cinemasList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all cinemas",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CinemaModel> Create(CinemaCreateModel cinemaCreateModel)
        {
            try
            {
                var cinema = PrepareCinema(cinemaCreateModel);

                await _cinemaRepository.InsertAsync(cinema);

                var cinemaModel = _mapper.Map<CinemaModel>(cinema);

                return cinemaModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new cinema, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create cinema", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CinemaModel> Update(int cinemaId, CinemaCreateModel cinemaUpdateModel)
        {
            try
            {
                var cinema = await _cinemaRepository.GetAsync(query => query
                    .Where(cinema => cinema.Id == cinemaId));

                if (cinema == null)
                    throw new BaseException($"Movie with id: {cinemaId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                _mapper.Map(cinemaUpdateModel, cinema);

                _cinemaRepository.Update(cinema);

                var cinemaModel = _mapper.Map<CinemaModel>(cinema);

                return cinemaModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update cinema with id: {cinemaId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CinemaModel> Delete(int cinemaId)
        {
            try
            {
                var cinema = await _cinemaRepository.GetAsync(query => query
                    .Where(cinema => cinema.Id == cinemaId));

                _cinemaRepository.Delete(cinema);

                return _mapper.Map<CinemaModel>(cinema);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update cinema with id: {cinemaId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private Cinema PrepareCinema(CinemaCreateModel cinemaCreateModel)
        {
            var cinema = _mapper.Map<Cinema>(cinemaCreateModel);
            //add authContextService to get user tracing later!
            cinema.InsertDate = DateTime.UtcNow;
            cinema.UpdateDate = DateTime.UtcNow;
            return cinema;
        }

    }
}
