using Movies.Core.Domain.Common;
using System;
using System.Collections.Generic;

namespace Movies.Core.Domain
{
    public class Event : BaseEntity, ISoftDeletedEntity
    {
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public int Price { get; set; }
        public int AttendeesNumber { get; set; }
        public bool Deleted { get; set; }
        public Cinema Cinema { get; set; }
    }
}
