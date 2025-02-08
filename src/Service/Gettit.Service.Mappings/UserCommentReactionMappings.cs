using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class UserCommentReactionMappings
    {
        public static UserCommentReactionServiceModel ToModel(this UserCommentReaction entity)
        {
            return new UserCommentReactionServiceModel
            {
                Id = entity.Id,
                Comment = entity.Comment?.ToModel(),
                Reaction = entity.Reaction?.ToModel(),
                User = entity.User?.ToModel()
            };
        }
    }
}
