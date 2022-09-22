using Movies.Core.Domain.Common;
using System.Collections.Generic;

namespace Movies.Core.Domain
{
    public class Seat : BaseEntity, ISoftDeletedEntity
    {
        public int CinemaId { get; set; }
        public int HallId { get; set; }
        public int RowId { get; set; }
        public string SeatName { get; set; }
        public bool IsVipSeat { get; set; }
        public bool IsSeatForCouple { get; set; }
        public bool Deleted { get; set; }

        public Hall Hall { get; set; }
        public Row Row { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
