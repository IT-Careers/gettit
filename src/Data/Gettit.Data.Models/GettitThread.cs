namespace Gettit.Data.Models
{
    public class GettitThread : MetadataBaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Category Category { get; set; }

        public List<UserThreadReaction> Reactions { get; set; }

        public List<UserThreadComment> Comment { get; set; }
    }
}
