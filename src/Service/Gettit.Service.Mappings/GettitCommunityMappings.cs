using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class GettitCommunityMappings
    {
        public static GettitCommunity ToEntity(this GettitCommunityServiceModel model)
        {
            return new GettitCommunity
            {
                Name = model.Name,
                Description = model.Description,
                Tags = model.Tags?.Select(tag => tag.ToEntity()).ToList(),
                ThumbnailPhoto = model.ThumbnailPhoto?.ToEntity(),
                BannerPhoto = model.BannerPhoto?.ToEntity()
            };
        }

        public static GettitCommunityServiceModel ToModel(this GettitCommunity entity)
        {
            return new GettitCommunityServiceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Tags = entity.Tags?.Select(tag => tag.ToModel()).ToList(),
                ThumbnailPhoto = entity.ThumbnailPhoto?.ToModel(),
                BannerPhoto = entity.BannerPhoto?.ToModel(),
                CreatedOn = entity.CreatedOn,
                UpdatedOn = entity.UpdatedOn,
                DeletedOn = entity.DeletedOn,
                CreatedBy = entity.CreatedBy.ToModel(),
                UpdatedBy = entity.UpdatedBy?.ToModel(),
                DeletedBy = entity.DeletedBy?.ToModel()
            };
        }
    }
}
