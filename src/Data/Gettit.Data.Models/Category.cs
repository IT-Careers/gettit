namespace Gettit.Data.Models
{
    public class Category : MetadataBaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Attachment CoverPhoto { get; set; }
    }
}
