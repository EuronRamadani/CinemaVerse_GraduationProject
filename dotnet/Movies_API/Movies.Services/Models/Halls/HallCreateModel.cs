using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Services.Models.Halls
{
    public class HallCreateModel
    {
        public int HallNumber { get; set; }
        //[MaxLength(15)]
        [Range(1, 15)]
        public int NumberOfRows { get; set; }
        public bool Has3D { get; set; }
    }
}
