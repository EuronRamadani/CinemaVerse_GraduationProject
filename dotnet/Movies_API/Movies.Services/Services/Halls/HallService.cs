using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Halls
{
    public class HallService : IHallService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Hall> _hallRepository;
        private readonly ILogger<HallService> _logger;

        public HallService(
            IMapper mapper,
            IRepository<Hall> hallRepository,
            ILogger<HallService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hallRepository = hallRepository ?? throw new ArgumentNullException(nameof(hallRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IList<HallListModel>> GetAllAsync()
        {
            try
            {
                var halls = await _hallRepository.GetAllAsync(query => query);

                var hallsList = _mapper.Map<IList<HallListModel>>(halls);

                return hallsList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all halls",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HallModel> GetAsync(int hallId)
        {
            var hall = await _hallRepository.GetAsync(query => query
               .Where(hall => hall.Id == hallId));

            if (hall == null)
                throw new BaseException($"Cinema with id {hallId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var hallModel = _mapper.Map<HallModel>(hall);

            return hallModel;
        }
        public async Task<HallModel> Create(HallCreateModel hallCreateModel)
        {
            try
            {
                var hall = PrepareHall(hallCreateModel);

                await _hallRepository.InsertAsync(hall);

                var hallModel = _mapper.Map<HallModel>(hall);

                return hallModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new hall, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create hall", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }
        public async Task<HallModel> Update(int hallId, HallCreateModel hallUpdateModel)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId));

                if (hall == null)
                    throw new BaseException($"Cinema with id: {hallId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                _mapper.Map(hallUpdateModel, hall);

                _hallRepository.Update(hall);

                var hallModel = _mapper.Map<HallModel>(hall);

                return hallModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update halls with id: {hallId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }
        public async Task<HallModel> Delete(int hallId)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId));

                _hallRepository.Delete(hall);

                return _mapper.Map<HallModel>(hall);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete hall with id: {hallId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }
        private Hall PrepareHall(HallCreateModel hallCreateModel)
        {
            var hall = _mapper.Map<Hall>(hallCreateModel);
            //add authContextService to get user tracing later!
            hall.InsertDate = DateTime.UtcNow;
            hall.UpdateDate = DateTime.UtcNow;
            return hall;
        }
    }
}
