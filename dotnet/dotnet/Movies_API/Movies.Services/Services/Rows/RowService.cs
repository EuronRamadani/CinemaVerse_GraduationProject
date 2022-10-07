using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Rows;
using Movies.Services.Models.Seats;
using Movies.Services.Services.Seats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movies.Services.Services.Rows
{
    public class RowService : IRowService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Row> _rowRepository;
        private readonly IRepository<Hall> _hallRepository;
        private readonly ISeatService _seatService;
        private readonly ILogger<RowService> _logger;

        public RowService(
            IMapper mapper,
            IRepository<Row> rowRepository,
            IRepository<Hall> hallRepository,
            ISeatService seatService,
            ILogger<RowService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _rowRepository = rowRepository ?? throw new ArgumentNullException(nameof(rowRepository));
            _hallRepository = hallRepository ?? throw new ArgumentNullException(nameof(hallRepository));
            _seatService = seatService ?? throw new ArgumentNullException(nameof(seatService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<RowModel>> GetAllAsync(int cinemaId, int hallId)
        {
            try
            {
                var rows = await _rowRepository.GetAllAsync(query => query
                    .Where(row => row.CinemaId == cinemaId)
                    .Where(row => row.HallId == hallId));

                var rowsList = _mapper.Map<IList<RowModel>>(rows);

                return rowsList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all rows",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RowModel> GetAsync(int cinemaId, int hallId, int rowId)
        {
            var row = await _rowRepository.GetAsync(query => query
                .Where(row => row.CinemaId == cinemaId)
                .Where(row => row.HallId == hallId)
                .Where(row => row.Id == rowId)
                .Include(row => row.Seats));

            if (row == null)
                throw new BaseException($"Row with id {rowId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var rowModel = _mapper.Map<RowModel>(row);

            return rowModel;
        }

        public async Task<RowModel> Create(int cinemaId, int hallId, RowCreateModel rowCreateModel)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId)
                    .Where(hall => hall.CinemaId == cinemaId));

                if (hall == null)
                    throw new BaseException($"Hall with id {hallId} not found!", ExceptionType.NotFound, HttpStatusCode.NotFound);

                var row = PrepareRow(rowCreateModel);

                row.CinemaId = cinemaId;
                row.HallId = hallId;

                await _rowRepository.InsertAsync(row);

                var rowModel = _mapper.Map<RowModel>(row);

                var seatList = await CreateSeats(cinemaId, hallId, row);

                row.Seats = seatList;

                _rowRepository.Update(row);

                return rowModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new row, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create row", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RowModel> Update(int cinemaId, int hallId, int rowId, RowCreateModel rowUpdateModel)
        {
            try
            {
                var rowModel = await GetAsync(cinemaId, hallId, rowId);

                _mapper.Map(rowUpdateModel, rowModel);

                var row = _mapper.Map<Row>(rowModel);

                _rowRepository.Update(row);

                return rowModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update Row with id: {rowId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RowModel> Delete(int cinemaId, int hallId, int rowId)
        {
            try
            {
                var rowModel = await GetAsync(cinemaId, hallId, rowId);

                var row = _mapper.Map<Row>(rowModel);

                _rowRepository.Delete(row);

                return _mapper.Map<RowModel>(row);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete Row with id: {rowId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<Seat>> CreateSeats(int cinemaId, int hallId, Row row)
        {
            var seatList = new List<Seat> { };

            for (int i = 0; i < row.NumberOfSeats; i++)
            {
                var seatCreateModel = new SeatCreateModel
                {
                    SeatName = null,
                    IsVipSeat = row.IsVipRow,
                    IsSeatForCouple = false,
                };

                if (i == 0 || i == row.NumberOfSeats - 1)
                {
                    seatCreateModel.IsSeatForCouple = true;
                }

                seatCreateModel.SeatName = $"{row.RowName + i}";

                try
                {
                    var seatModel = await _seatService.Create(cinemaId, hallId, row.Id, seatCreateModel);

                    seatList.Add(_mapper.Map<Seat>(seatModel));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);

                    if (e is BaseException) throw;
                    throw new BaseException($"Failed to insert seat!",
                        ExceptionType.ServerError, HttpStatusCode.InternalServerError);
                }
            };

            return seatList;
        }

        private Row PrepareRow(RowCreateModel rowCreateModel)
        {
            var row = _mapper.Map<Row>(rowCreateModel);
            row.InsertDate = DateTime.UtcNow;
            row.UpdateDate = DateTime.UtcNow;
            return row;
        }
    }
}
