﻿namespace Gettit.Service.Models
{
    public class GettitThreadServiceModel : MetadataBaseServiceModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public GettitCommunityServiceModel Community { get; set; }

        public List<GettitTagServiceModel> Tags { get; set; }
    }
}
