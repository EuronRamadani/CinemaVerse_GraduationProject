namespace Movies.Services.Models.Seats
{
    public class SeatModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public int HallId { get; set; }
        public int RowId { get; set; }
        public string SeatName { get; set; }
        public bool IsVipSeat { get; set; }
        public bool IsSeatForCouple { get; set; }
    }
}
