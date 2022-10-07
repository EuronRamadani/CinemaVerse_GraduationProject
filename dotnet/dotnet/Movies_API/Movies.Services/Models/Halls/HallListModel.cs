namespace Movies.Services.Models.Halls
{
    public class HallListModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public string HallNumber { get; set; }
        public int NumberOfRows { get; set; }
        public bool Has3D { get; set; }
    }
}
