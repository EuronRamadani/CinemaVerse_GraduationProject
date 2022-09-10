using Movies.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Domain
{
    public class Actor : BaseEntity, ISoftDeletedEntity
    {
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string ImgPath { get; set; }
        public string Nationality { get; set; }
        public string  Genre{ get; set; }
        public DateTime Birth{ get; set; }
        public bool Deleted { get; set; }

        //public List<Movie> Movies { get; set; }
    }
}
