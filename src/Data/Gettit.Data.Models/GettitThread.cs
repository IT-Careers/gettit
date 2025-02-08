namespace Gettit.Data.Models
{
    public class GettitThread : MetadataBaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public List<Attachment> Attachments { get; set; }

        public GettitCommunity Community { get; set; }

        public List<GettitTag> Tags { get; set; }

        public List<UserThreadReaction> Reactions { get; set; }

        public List<UserThreadComment> Comments { get; set; }
    }
}
