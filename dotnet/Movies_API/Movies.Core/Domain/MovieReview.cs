using Movies.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Domain
{
    public class MovieReview : BaseEntity, ISoftDeletedEntity
    {
        public int MovieId { get; set; }

        public string ReviewTitle { get; set;}

        public string ReviewDescription { get; set;}

        public int ReviewRating { get; set;}

        public DateTime ReviewDate { get; set; }

        public bool Deleted { get; set; }

        public Movie Movie { get; set; }
    }
}
