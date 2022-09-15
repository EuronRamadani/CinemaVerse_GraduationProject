using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Models.Reviews
{
    public class MovieReviewListModel
    {
        public int Id { get; set; }
        public string ReviewTitle { get; set; }

        public string ReviewDescription { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
