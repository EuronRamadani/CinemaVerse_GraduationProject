using Movies.Core.Domain.Common;
using System;

namespace Movies.Core
{
    public abstract class BaseEntity : IPrimaryEntity
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid InsertedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
