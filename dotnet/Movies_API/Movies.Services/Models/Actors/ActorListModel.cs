using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Models.Actors
{
    public class ActorListModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgPath { get; set; }
        public string Nationality { get; set; }
        public string Genre { get; set; }
        public DateTime Birth { get; set; }
    }
}
