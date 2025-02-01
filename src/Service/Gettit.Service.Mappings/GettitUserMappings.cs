using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class GettitUserMappings
    {
        public static GettitUser ToEntity(this GettitUserServiceModel model)
        {
            return new GettitUser(); // TODO: Verify if we actually need this
        }

        public static GettitUserServiceModel ToModel(this GettitUser entity)
        {
            return new GettitUserServiceModel
            {
                ForumRole = entity.ForumRole?.ToModel(),
                Email = entity.Email,
                Id = entity.Id,
                UserName = entity.UserName
            };
        }
    }
}
