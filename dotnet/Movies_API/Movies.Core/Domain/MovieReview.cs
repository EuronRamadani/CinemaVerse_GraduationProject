using Movies.Core.Domain.Common;

namespace Movies.Core.Domain
{
    public class MovieReview : BaseEntity, ISoftDeletedEntity
    {
        public int MovieId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewRating { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Deleted { get; set; }

        public Movie Movie { get; set; }
    }
}
