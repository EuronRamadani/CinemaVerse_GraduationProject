using System;

namespace Movies.Services.Models.MovieTimes
{
    public class MovieTimeCreateModel
    {
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
