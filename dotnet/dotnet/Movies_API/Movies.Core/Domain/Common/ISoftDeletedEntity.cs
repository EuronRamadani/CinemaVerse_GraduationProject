namespace Movies.Core.Domain.Common
{
    public partial interface ISoftDeletedEntity
    {
        bool Deleted { get; set; }
    }
}
