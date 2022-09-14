using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Models.Halls
{
    public class HallModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public int HallNumber { get; set; }
        public int NumOfSeats { get; set; }
    }
}
