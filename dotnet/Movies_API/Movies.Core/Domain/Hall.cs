using Movies.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Domain
{
    public class Hall : BaseEntity, ISoftDeletedEntity
    {
        public int HallNumber{ get; set; }
        public int NumOfSeats{ get; set; }
        public bool Deleted{ get; set; }
        public Cinema Cinema{ get; set; }
    }
}
