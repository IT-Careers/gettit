namespace Gettit.Data.Models
{
    public abstract class MetadataBaseEntity : BaseEntity
    {
        public GettitUser CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public GettitUser? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public GettitUser? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
