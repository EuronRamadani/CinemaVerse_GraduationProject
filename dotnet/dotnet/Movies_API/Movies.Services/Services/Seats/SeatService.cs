using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Seats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Seats
{
    public class SeatService : ISeatService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IRepository<Row> _rowRepository;
        private readonly IRepository<Hall> _hallRepository;
        private readonly ILogger<SeatService> _logger;

        public SeatService(
            IMapper mapper,
            IRepository<Seat> seatRepository,
            IRepository<Row> rowRepository,
            IRepository<Hall> hallRepository,
            ILogger<SeatService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
            _rowRepository = rowRepository ?? throw new ArgumentNullException(nameof(rowRepository));
            _hallRepository = hallRepository ?? throw new ArgumentNullException(nameof(hallRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<SeatModel>> GetAllAsync(int cinemaId, int hallId, int rowId)
        {
            try
            {
                var seats = await _seatRepository.GetAllAsync(query => query
                    .Where(seat => seat.CinemaId == cinemaId)
                    .Where(seat => seat.HallId == hallId)
                    .Where(seat => seat.RowId == rowId));

                var seatsList = _mapper.Map<IList<SeatModel>>(seats);

                return seatsList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all seats",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SeatModel> GetAsync(int cinemaId, int hallId, int seatId)
        {
            var seat = await _seatRepository.GetAsync(query => query
                .Where(seat => seat.CinemaId == cinemaId)
                .Where(seat => seat.HallId == hallId)
                .Where(seat => seat.Id == seatId));

            if (seat == null)
                throw new BaseException($"Seat with id {seatId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var seatModel = _mapper.Map<SeatModel>(seat);

            return seatModel;
        }

        public async Task<SeatModel> Create(int cinemaId, int hallId, int rowId, SeatCreateModel seatCreateModel)
        {
            try
            {
                var row = await _rowRepository.GetAsync(query => query
                    .Where(row => row.CinemaId == cinemaId)
                    .Where(row => row.HallId == hallId)
                    .Where(row => row.Id == rowId));

                if (row == null)
                    throw new BaseException($"Row with id {rowId} not found!", ExceptionType.NotFound, HttpStatusCode.NotFound);

                var seat = PrepareSeat(seatCreateModel);

                seat.CinemaId = cinemaId;
                seat.HallId = hallId;
                seat.RowId = rowId;

                await _seatRepository.InsertAsync(seat);

                var seatModel = _mapper.Map<SeatModel>(seat);

                return seatModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new seat, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create seat", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SeatModel> Update(int cinemaId, int hallId, int seatId, SeatCreateModel seatUpdateModel)
        {
            try
            {
                var seatModel = await GetAsync(cinemaId, hallId, seatId);

                _mapper.Map(seatUpdateModel, seatModel);

                var seat = _mapper.Map<Seat>(seatModel);

                _seatRepository.Update(seat);

                return seatModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update Seat with id: {seatId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SeatModel> Delete(int cinemaId, int hallId, int seatId)
        {
            try
            {
                var seatModel = await GetAsync(cinemaId, hallId, seatId);

                var seat = _mapper.Map<Seat>(seatModel);

                _seatRepository.Delete(seat);

                return _mapper.Map<SeatModel>(seat);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete Seat with id: {seatId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private Seat PrepareSeat(SeatCreateModel seatCreateModel)
        {
            var seat = _mapper.Map<Seat>(seatCreateModel);
            seat.InsertDate = DateTime.UtcNow;
            seat.UpdateDate = DateTime.UtcNow;
            return seat;
        }
    }
}
