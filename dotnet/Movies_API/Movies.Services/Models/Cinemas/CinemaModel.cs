using Movies.Core.Domain;
using System.Collections.Generic;

namespace Movies.Services.Models.Cinemas
{
    public class CinemaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int NumberOfVenues { get; set; }
    }
}
