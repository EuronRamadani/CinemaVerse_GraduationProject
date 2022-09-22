namespace Movies.Services.Models.Seats
{
    public class SeatCreateModel
    {
        public string SeatName { get; set; }
        public bool IsVipSeat { get; set; }
        public bool IsSeatForCouple { get; set; }
    }
}
