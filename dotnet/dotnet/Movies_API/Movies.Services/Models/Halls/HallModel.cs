using Movies.Services.Models.Rows;
using System.Collections.Generic;

namespace Movies.Services.Models.Halls
{
    public class HallModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public string HallNumber { get; set; }
        public int NumberOfRows { get; set; }
        public bool Has3D { get; set; }

        public List<RowModel> Rows{ get; set; }
    }
}
