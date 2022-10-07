using Movies.Services.Models.Halls;
using System;

namespace Movies.Services.Models.MovieTimes
{
    public class MovieTimeModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public HallModel Hall { get; set; }
    }
}
