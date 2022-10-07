using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Halls;
using Movies.Services.Models.Rows;
using Movies.Services.Services.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movies.Services.Services.Halls
{
    public class HallService : IHallService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Hall> _hallRepository;
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRowService _rowService;
        private readonly ILogger<HallService> _logger;

        public HallService(
            IMapper mapper,
            IRepository<Hall> hallRepository,
            IRepository<Cinema> cinemaRepository,
            IRowService rowService,
            ILogger<HallService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hallRepository = hallRepository ?? throw new ArgumentNullException(nameof(hallRepository));
            _cinemaRepository = cinemaRepository ?? throw new ArgumentNullException(nameof(cinemaRepository));
            _rowService = rowService ?? throw new ArgumentNullException(nameof(rowService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<HallListModel>> GetAllAsync(int cinemaId)
        {
            try
            {
                var halls = await _hallRepository.GetAllAsync(query => query
                    .Where(halls => halls.Cinema.Id == cinemaId));

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

        public async Task<HallModel> GetAsync(int cinemaId, int hallId)
        {
            var hall = await _hallRepository.GetAsync(query => query
                .Where(hall => hall.Id == hallId)
                .Where(hall => hall.CinemaId == cinemaId)
                .Include(hall => hall.Rows)
                    .ThenInclude(Row => Row.Seats));

            if (hall == null)
                throw new BaseException($"Hall with id {hallId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var hallModel = _mapper.Map<HallModel>(hall);

            return hallModel;
        }


        public async Task<HallModel> Create(int cinemaId, HallCreateModel hallCreateModel)
        {
            try
            {
                var cinema = await _cinemaRepository.GetAsync(query => query
                    .Where(c => c.Id == cinemaId));

                if (cinema == null)
                    throw new BaseException($"Cinema with id {cinemaId} not found!", ExceptionType.NotFound, HttpStatusCode.NotFound);

                var hall = PrepareHall(hallCreateModel);

                hall.Cinema = cinema;
                hall.CinemaId = cinemaId;

                await _hallRepository.InsertAsync(hall);

                var hallModel = _mapper.Map<HallModel>(hall);

                var rowList = await CreateRows(cinemaId, hallModel);

                hallModel.Rows = _mapper.Map<List<RowModel>>(rowList);

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

        public async Task<HallModel> Update(int cinemaId, int hallId, HallCreateModel hallUpdateModel)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId)
                    .Where(hall => hall.CinemaId == cinemaId));

                if (hall == null)
                    throw new BaseException($"Hall with id: {hallId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);
                
                var numberOfRows = hall.NumberOfRows;
                
                _mapper.Map(hallUpdateModel, hall);

                hall.NumberOfRows = numberOfRows;

                _hallRepository.Update(hall);

                var hallModel = _mapper.Map<HallModel>(hall);

                return hallModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update Hall with id: {hallId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HallModel> Delete(int cinemaId, int hallId)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId)
                    .Where(hall => hall.CinemaId == cinemaId));

                _hallRepository.Delete(hall);

                return _mapper.Map<HallModel>(hall);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete Hall with id: {hallId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<Row>> CreateRows(int cinemaId, HallModel hallModel)
        {
            var rowList = new List<Row> { };
            var rowIdCodes = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P' };

            for (int i = 0; i < hallModel.NumberOfRows; i++)
            {
                var rowCreateModel = new RowCreateModel
                {
                    RowName = null,
                    NumberOfSeats = 15,
                    IsVipRow = false,
                };

                if (i == hallModel.NumberOfRows - 1)
                {
                    rowCreateModel.NumberOfSeats = 10;
                    rowCreateModel.IsVipRow = true;
                }

                rowCreateModel.RowName = rowIdCodes[i].ToString();

                try
                {
                    var rowModel = await _rowService.Create(cinemaId, hallModel.Id, rowCreateModel);
                    
                    rowList.Add(_mapper.Map<Row>(rowModel));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);

                    if (e is BaseException) throw;
                    throw new BaseException($"Failed to insert row!",
                        ExceptionType.ServerError, HttpStatusCode.InternalServerError);
                }
            };

            return rowList;
        }

        private Hall PrepareHall(HallCreateModel hallCreateModel)
        {
            var hall = _mapper.Map<Hall>(hallCreateModel);
            hall.InsertDate = DateTime.UtcNow;
            hall.UpdateDate = DateTime.UtcNow;
            return hall;
        }
    }
}
