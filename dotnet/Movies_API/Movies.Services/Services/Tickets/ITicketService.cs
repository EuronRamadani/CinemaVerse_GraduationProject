using Movies.Services.Models.Tickets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Tickets
{
    public interface ITicketService
    {
        Task<IList<TicketModel>> GetAllAsync(int cinemaId, int hallId, int movieTimeId);
        Task<IList<TicketModel>> GetAllUserTicketsAsync(string userId);
        Task<TicketModel> GetAsync(int cinemaId, int hallId, int ticketId);
        Task<List<TicketModel>> Create(int cinemaId, int hallId, int movieTimeId, List<int> seatsId);
        Task<List<TicketModel>> ReserveTickets(int cinemaId, int hallId, int movieTimeId, List<int> ticketsId, string ownerId);
        //Task<TicketModel> Update(int cinemaId, int hallId, int ticketId, TicketCreateModel ticketModel);
        Task<TicketModel> Delete(int cinemaId, int hallId, int ticketId);
    }
}
