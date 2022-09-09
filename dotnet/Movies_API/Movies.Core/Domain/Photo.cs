using Movies.Core.Domain.Common;
using System;

namespace Movies.Core.Domain
{
    public class Photo : BaseEntity, ISoftDeletedEntity
    {
        public Guid LongId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public bool Deleted { get; set; }
    }
}
