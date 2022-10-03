using System;

namespace Movies.Services.Models.Events
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
    }
}
