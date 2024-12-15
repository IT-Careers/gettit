﻿namespace Gettit.Service.Models
{
    public class GettitRoleServiceModel : BaseServiceModel
    {
        public const string GettitRoleDefaultAuthority = "User";

        public string Label { get; set; }

        // CSS Color configuration
        public string Color { get; set; }

        public string Authority { get; set; } = GettitRoleDefaultAuthority;
    }
}