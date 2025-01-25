namespace Gettit.Data.Models
{
    public class GettitRole : MetadataBaseEntity
    {
        public const string GettitRoleDefaultAuthority = "User";

        public string Label { get; set; }

        // CSS Color configuration
        public string Color { get; set; }

        public string Authority { get; set; } = GettitRoleDefaultAuthority;
    }
}
