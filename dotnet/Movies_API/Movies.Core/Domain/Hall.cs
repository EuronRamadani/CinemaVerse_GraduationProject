using Movies.Core.Domain.Common;
using System.Collections.Generic;

namespace Movies.Core.Domain
{
    public class Hall : BaseEntity, ISoftDeletedEntity
    {
        public int CinemaId { get; set; }
        public int HallNumber{ get; set; }
        public int NumOfSeats{ get; set; }
        public bool Deleted{ get; set; }

        public Cinema Cinema{ get; set; }
        public List<MovieTime> MovieTimes { get; set; }
    }
}
