using Movies.Services.Models.Seats;
using System.Collections.Generic;

namespace Movies.Services.Models.Rows
{
    public class RowModel
    {
        public int CinemaId { get; set; }
        public int HallId { get; set; }
        public string RowName { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsVipRow { get; set; }

        public List<SeatModel> Seats { get; set; }
    }
}
