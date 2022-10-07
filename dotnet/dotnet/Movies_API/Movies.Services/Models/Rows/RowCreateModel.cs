namespace Movies.Services.Models.Rows
{
    public class RowCreateModel
    {
        public string RowName { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsVipRow { get; set; } = false;
    }
}
