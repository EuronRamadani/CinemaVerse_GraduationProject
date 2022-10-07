using Movies.Core.Domain.Common;
using System;

namespace Movies.Core.Domain
{
    public class MovieTime : BaseEntity, ISoftDeletedEntity
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Deleted { get; set; }

        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
    }
}
