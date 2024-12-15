using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class CategoryMappings
    {
        public static Category ToEntity(this CategoryServiceModel model)
        {
            return new Category
            {
                Name = model.Name,
                Description = model.Description,
                CoverPhoto = model.CoverPhoto?.ToEntity()
            };
        }

        public static CategoryServiceModel ToModel(this Category entity)
        {
            return new CategoryServiceModel
            {
                Name = entity.Name,
                Description = entity.Description,
                CoverPhoto = entity.CoverPhoto.ToModel(),
                CreatedOn = entity.CreatedOn,
                UpdatedOn = entity.UpdatedOn,
                DeletedOn = entity.DeletedOn,
                CreatedBy = entity.CreatedBy.ToModel(),
                UpdatedBy = entity.UpdatedBy.ToModel(),
                DeletedBy = entity.DeletedBy.ToModel()
            };
        }
    }
}
