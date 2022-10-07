using Movies.Core.Domain.Common;
using System;

namespace Movies.Core.Domain
{
    public class Ticket : BaseEntity, ISoftDeletedEntity
    {
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
        public bool Deleted { get; set; }

        public Seat Seat { get; set; }
        public MovieTime MovieTime { get; set; }
        public Hall Hall { get; set; }
    }
}
