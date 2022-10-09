using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Hall> _hallRepository;
        private readonly IRepository<MovieTime> _movieTimeRepository;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly ILogger<TicketService> _logger;

        public TicketService(
            IMapper mapper,
            IRepository<Hall> hallRepository,
            IRepository<MovieTime> movieTimeRepository,
            IRepository<Seat> seatRepository,
            IRepository<Ticket> ticketRepository,
            ILogger<TicketService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hallRepository = hallRepository ?? throw new ArgumentNullException(nameof(hallRepository));
            _movieTimeRepository = movieTimeRepository ?? throw new ArgumentNullException(nameof(movieTimeRepository));
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<TicketModel>> GetAllAsync(int cinemaId, int hallId, int movieTimeId)
        {
            try
            {
                var tickets = await _ticketRepository.GetAllAsync(query => query
                    .Where(ticket => ticket.Hall.CinemaId == cinemaId)
                    .Where(ticket => ticket.HallId == hallId)
                    .Where(ticket => ticket.MovieTimeId == movieTimeId));

                var ticketsList = _mapper.Map<IList<TicketModel>>(tickets);

                return ticketsList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all tickets for specified movie time",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }       
        
        public async Task<IList<TicketModel>> GetAllByTicketsIdAsync(int cinemaId, int hallId, int movieTimeId, List<int> ticketsId)
        {
            try
            {
                var tickets = await _ticketRepository.GetAllAsync(query => query
                    .Where(ticket => ticket.Hall.CinemaId == cinemaId)
                    .Where(ticket => ticket.HallId == hallId)
                    .Where(ticket => ticket.MovieTimeId == movieTimeId)
                    .Where(ticket => ticketsId.Contains(ticket.Id)).AsNoTracking());

                var ticketsList = _mapper.Map<IList<TicketModel>>(tickets);

                return ticketsList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all tickets by tickets id",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<TicketModel> GetAsync(int cinemaId, int hallId, int ticketId)
        {
            var ticket = await _ticketRepository.GetAsync(query => query
                .Where(ticket => ticket.Hall.CinemaId == cinemaId)
                .Where(ticket => ticket.HallId == hallId)
                .Where(ticket => ticket.Id == ticketId));

            if (ticket == null)
                throw new BaseException($"Ticket with id {ticketId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var ticketModel = _mapper.Map<TicketModel>(ticket);

            return ticketModel;
        }

        public async Task<List<TicketModel>> Create(int cinemaId, int hallId, int movieTimeId, List<int> seatsId)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId)
                    .Where(hall => hall.CinemaId == cinemaId)
                    .Include(hall => hall.Rows));

                if (hall == null)
                    throw new BaseException($"Hall with id {hallId} not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var movieTime = await _movieTimeRepository.GetAsync(query => query
                    .Include(movieTime => movieTime.Movie)
                    .Where(movieTime => movieTime.Movie.CinemaId == cinemaId)
                    .Where(movieTime => movieTime.Id == movieTimeId));

                if (movieTime == null)
                    throw new BaseException($"Movie Time with id {movieTime} not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var seats = await _seatRepository.GetAllAsync(query => query
                    .Where(seat => seat.CinemaId == cinemaId)
                    .Where(seat => seat.HallId == hallId)
                    .Where(seat => seatsId.Contains(seat.Id))
                    .Include(seat => seat.Row)
                    .Include(seat => seat.Tickets));

                if (seats == null || seats.Count == 0)
                    throw new BaseException($"Requested Seats were not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var tickets = PrepareTickets(hall, movieTime, seats);

                await _ticketRepository.InsertAsync(tickets);

                var ticketsModel = _mapper.Map<List<TicketModel>>(tickets);

                return ticketsModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new ticket, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create seat", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<TicketModel>> ReserveTickets(int cinemaId, int hallId, int movieTimeId, List<int> ticketsId, string ownerId)
        {
            try
            {
                var hall = await _hallRepository.GetAsync(query => query
                    .Where(hall => hall.Id == hallId)
                    .Where(hall => hall.CinemaId == cinemaId));

                if (hall == null)
                    throw new BaseException($"Hall with id {hallId} not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var movieTime = await _movieTimeRepository.GetAsync(query => query
                    .Include(movieTime => movieTime.Movie)
                    .Where(movieTime => movieTime.Movie.CinemaId == cinemaId)
                    .Where(movieTime => movieTime.Id == movieTimeId));

                if (movieTime == null)
                    throw new BaseException($"Movie Time with id {movieTime} not found!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var tickets = await _ticketRepository.GetAllAsync(query => query
                    .Where(ticket => ticket.HallId == hallId)
                    .Where(ticket => ticket.MovieTimeId == movieTimeId)
                    .Where(ticket => ticketsId.Contains(ticket.Id)).AsNoTracking());

                if ((tickets == null || tickets.Count == 0) || tickets.Any(ticket => !ticket.IsAvailable))
                    throw new BaseException($"Requested Tickets were not available!", ExceptionType.ServerError,
                        HttpStatusCode.NotFound);

                var ticketsModel = await GetAllByTicketsIdAsync(cinemaId, hallId, movieTimeId, ticketsId);

                var ticketsList = _mapper.Map<List<Ticket>>(ticketsModel);

                //handle payment

                MakeTicketsUnAvailable(ticketsList, ownerId);

                _ticketRepository.Update(ticketsList);

                return ticketsModel.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to reserve Tickets!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private void MakeTicketsUnAvailable(List<Ticket> tickets, string ownerId) 
        {
            foreach (var ticket in tickets)
            {
                ticket.IsAvailable = false;
                //get owner id
                ticket.OwnerId = ownerId;
            }
        }

        public async Task<TicketModel> MakeTicketAvailable(int cinemaId, int hallId, int ticketId)
        {
            try
            {
                var ticketModel = await GetAsync(cinemaId, hallId, ticketId);

                ticketModel.IsAvailable = true;
                ticketModel.OwnerId = null;

                var ticket = _mapper.Map<Ticket>(ticketModel);

                _ticketRepository.Update(ticket);

                return _mapper.Map<TicketModel>(ticket);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to make Ticket with id: {ticketId} available!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<TicketModel> Delete(int cinemaId, int hallId, int ticketId)
        {
            try
            {
                var ticketModel = await GetAsync(cinemaId, hallId, ticketId);

                var ticket = _mapper.Map<Ticket>(ticketModel);

                _ticketRepository.Delete(ticket);

                return _mapper.Map<TicketModel>(ticket);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete Ticket with id: {ticketId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        private List<Ticket> PrepareTickets(Hall hall, MovieTime movieTime, IList<Seat> seats)
        {
            var ticketsList = new List<Ticket>(); 

            foreach (var seat in seats)
            {
                var ticket = new Ticket {
                    TicketCode = Guid.NewGuid(),
                    HallId = hall.Id,
                    MovieTimeId = movieTime.Id,
                    SeatId = seat.Id,
                    RowId = seat.RowId,
                    MovieStartTime = movieTime.StartTime,
                    //get user id
                    OwnerId = null,
                    IsAvailable = true,
                    Is3D = hall.Has3D,
                    IsVipTicket = seat.IsVipSeat,
                    IsTicketForCouple = seat.IsSeatForCouple,
                    Price = GetTicketPrice(hall, seat),
                    InsertDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                };
                seat.Tickets.Add(ticket);

                ticketsList.Add(ticket);
            }

            return ticketsList;
        }
        
        private double GetTicketPrice(Hall hall, Seat seat)
        {
            double basePrice = 2.5;

            double calculatedPrice = basePrice;

            if (hall.Has3D)
                calculatedPrice = calculatedPrice + 1.0;
            
            if (seat.IsVipSeat)
                calculatedPrice = calculatedPrice + 2.0;            
            
            if (seat.IsSeatForCouple)
                calculatedPrice = calculatedPrice * 2;

            return calculatedPrice;
        }
    }
}
