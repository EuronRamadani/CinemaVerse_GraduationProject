using Movies.Services.Models.Photos;
using System;
using System.Collections.Generic;

namespace Movies.Services.Models.Events
{
    public class EventListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public int AttendeesNumber { get; set; }
        public IList<PhotoModel> Photos { get; set; }

    }
}
