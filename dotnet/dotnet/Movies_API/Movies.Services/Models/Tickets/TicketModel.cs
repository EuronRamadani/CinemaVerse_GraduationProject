using System;

namespace Movies.Services.Models.Tickets
{
    public class TicketModel
    {
        public int Id { get; set; }
        public Guid TicketCode { get; set; }
        public int HallId { get; set; }
        public int MovieTimeId { get; set; }
        public int SeatId { get; set; }
        public int RowId { get; set; }
        public DateTime MovieStartTime { get; set; }
        public string OwnerId { get; set; }
        public bool IsAvailable { get; set; }
        public bool Is3D { get; set; }
        public bool IsVipTicket { get; set; }
        public bool IsTicketForCouple { get; set; }
        public double Price { get; set; }
    }
}
