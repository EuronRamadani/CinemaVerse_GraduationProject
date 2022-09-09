using Movies.Core.Domain.Common;
using System.Collections.Generic;

namespace Movies.Core.Domain
{
    public class Cinema : BaseEntity, ISoftDeletedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int NumberOfVenues { get; set; }
        public bool Deleted { get; set; }

        public List<Movie> Movies { get; set; }
        public List<Photo> Photos { get; set; }
        //Add venues later
        //public List<Venue> Venues{ get; set; }
    }
}
