namespace Gettit.Data.Models
{
    public class GettitCommunity : MetadataBaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        // Threads in this community can only hold the community tags
        public List<GettitTag> Tags { get; set; }

        public Attachment? ThumbnailPhoto { get; set; }
        
        public Attachment? BannerPhoto { get; set; }
    }
}
