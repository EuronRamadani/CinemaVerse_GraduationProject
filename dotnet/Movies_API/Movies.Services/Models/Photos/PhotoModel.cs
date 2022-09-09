using System;

namespace Movies.Services.Models.Photos
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public Guid LongId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
    }
}
