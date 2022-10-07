using Movies.Services.Models.Seats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Seats
{
    public interface ISeatService
    {
        Task<IList<SeatModel>> GetAllAsync(int cinemaId, int hallId, int rowId);
        Task<SeatModel> GetAsync(int cinemaId, int hallId, int seatId);
        Task<SeatModel> Create(int cinemaId, int hallId, int rowId, SeatCreateModel seatCreateModel);
        Task<SeatModel> Update(int cinemaId, int hallId, int seatId, SeatCreateModel seatModel);
        Task<SeatModel> Delete(int cinemaId, int hallId, int seatId);
    }
}
