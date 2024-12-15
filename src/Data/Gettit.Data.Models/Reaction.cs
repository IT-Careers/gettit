namespace Gettit.Data.Models
{
    public class Reaction : MetadataBaseEntity
    {
        public string Label { get; set; }

        public Attachment Emote { get; set; }
    }
}
