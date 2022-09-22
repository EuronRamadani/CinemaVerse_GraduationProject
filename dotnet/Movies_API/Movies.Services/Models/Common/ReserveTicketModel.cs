using System.Collections.Generic;

namespace Movies.Services.Models.Common
{
    public class ReserveTicketModel
    {
        public List<int> TicketsId { get; set; }
        public string OwnerId { get; set; }
    }
}
