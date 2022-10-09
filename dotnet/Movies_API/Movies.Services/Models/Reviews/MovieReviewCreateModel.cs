using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Models.Reviews
{
    public class MovieReviewCreateModel
    {
        public string ReviewTitle { get; set; }

        public string ReviewDescription { get; set; }

        public int ReviewRating { get; set; }
        public string UserId { get; set; }

    }
}
