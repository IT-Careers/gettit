using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class GettitRoleMappings
    {
        public static GettitRole ToEntity(this GettitRoleServiceModel model)
        {
            return new GettitRole
            {
                Label = model.Label,
                Color = model.Color,
                Authority = model.Authority
            };
        }

        public static GettitRoleServiceModel ToModel(this GettitRole entity)
        {
            return new GettitRoleServiceModel
            {
                Id = entity.Id,
                Label = entity.Label,
                Color = entity.Color,
                Authority = entity.Authority,
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
