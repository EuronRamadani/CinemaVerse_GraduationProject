using System;

namespace Movies.Services.Models.Events
{
    public class EventCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
