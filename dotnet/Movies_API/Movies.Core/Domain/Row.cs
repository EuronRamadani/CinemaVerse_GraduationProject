using Movies.Core.Domain.Common;
using System.Collections.Generic;

namespace Movies.Core.Domain
{
    public class Row : BaseEntity, ISoftDeletedEntity
    {
        public int CinemaId { get; set; }
        public int HallId { get; set; }
        public string RowName { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsVipRow { get; set; }
        public bool Deleted { get; set; }

        public Hall Hall { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
