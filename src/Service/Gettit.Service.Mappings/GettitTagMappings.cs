using Gettit.Data.Models;
using Gettit.Service.Models;
using System.Reflection.Emit;

namespace Gettit.Service.Mappings
{
    public static class GettitTagMappings
    {
        public static GettitTag ToEntity(this GettitTagServiceModel model)
        {
            return new GettitTag
            {
                Label = model.Label
            };
        }

        public static GettitTagServiceModel ToModel(this GettitTag entity)
        {
            return new GettitTagServiceModel
            {
                Id = entity.Id,
                Label = entity.Label,
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
