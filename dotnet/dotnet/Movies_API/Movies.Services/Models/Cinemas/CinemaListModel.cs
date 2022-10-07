using Movies.Services.Models.Photos;
using System.Collections.Generic;

namespace Movies.Services.Models.Cinemas
{
    public class CinemaListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int NumberOfVenues { get; set; }
        public IList<PhotoModel> Photos { get; set; }
    }
}
