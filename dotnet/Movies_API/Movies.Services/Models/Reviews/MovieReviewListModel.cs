using System;

namespace Movies.Services.Models.Reviews
{
    public class MovieReviewListModel
    {
        public int Id { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewRating { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
