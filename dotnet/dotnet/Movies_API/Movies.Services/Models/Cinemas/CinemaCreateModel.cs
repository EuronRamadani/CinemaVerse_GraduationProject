namespace Movies.Services.Models.Cinemas
{
    public class CinemaCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int NumberOfVenues { get; set; }
    }
}
