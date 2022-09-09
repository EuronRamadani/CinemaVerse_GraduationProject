using Movies.Core.Domain;

namespace Movies.Services.Models.Movies
{
    public class MovieListModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Title { get; set; }
        public string TrailerLink { get; set; }
        public int ReleaseYear { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
    }
}
