namespace Gettit.Service.Models
{
    public class GettitCommunityServiceModel : MetadataBaseServiceModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<GettitTagServiceModel> Tags { get; set; }

        public AttachmentServiceModel ThumbnailPhoto { get; set; }

        public AttachmentServiceModel BannerPhoto { get; set; }
    }
}
