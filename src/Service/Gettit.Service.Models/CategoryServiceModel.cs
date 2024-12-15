namespace Gettit.Service.Models
{
    public class CategoryServiceModel : MetadataBaseServiceModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public AttachmentServiceModel CoverPhoto { get; set; }
    }
}
