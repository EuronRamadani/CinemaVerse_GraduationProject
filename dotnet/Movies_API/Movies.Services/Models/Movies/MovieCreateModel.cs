using System;
using System.Collections.Generic;

namespace Movies.Services.Models.Movies
{
    public class MovieCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public List<int> ActorIds { get; set; }
        public string TrailerLink { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        //Change to List of Genre class later
        public string Genre { get; set; }
        public int Length { get; set; }
    }
}
