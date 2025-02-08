namespace Gettit.Service.Models
{
    public class ReactionServiceModel : MetadataBaseServiceModel
    {
        public string Label { get; set; }

        public AttachmentServiceModel Emote { get; set; }
    }
}
