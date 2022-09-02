using Movies.Core.Domain.Common;
using System;

namespace Movies.Core.Domain
{
    public class Movie : BaseEntity, ISoftDeletedEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string TrailerLink { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        //Change to List of Genre class later
        public string Genre { get; set; }
        public int Length { get; set; }
        public bool Deleted { get; set; }

        //public List<Actor> Actors { get; set; }
        //public List<Review> Reviews { get; set; }
    }

    //public class Actor
    //{
    //    public string CharacterName { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //}
}
