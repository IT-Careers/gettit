using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public enum UserThreadCommentMappingsContext
    {
        Thread,
        Comment,
        User
    }

    public static class UserThreadCommentMappings
    {
        public static UserThreadCommentServiceModel ToModel(this UserThreadComment entity, UserThreadCommentMappingsContext context)
        {
            return new UserThreadCommentServiceModel
            {
                Id = entity.Id,
                Comment = ShouldMapComments(context) ? entity.Comment?.ToModel(CommentMappingsContext.Parent) : null,
                Thread = ShouldMapThread(context) ? entity.Thread?.ToModel() : null,
                User = ShouldMapUser(context) ? entity.User?.ToModel() : null
            };
        }

        private static bool ShouldMapComments(UserThreadCommentMappingsContext context)
        {
            return context == UserThreadCommentMappingsContext.Thread || context == UserThreadCommentMappingsContext.User;
        }

        private static bool ShouldMapThread(UserThreadCommentMappingsContext context)
        {
            return context == UserThreadCommentMappingsContext.Comment || context == UserThreadCommentMappingsContext.User;
        }

        private static bool ShouldMapUser(UserThreadCommentMappingsContext context)
        {
            return context == UserThreadCommentMappingsContext.Thread || context == UserThreadCommentMappingsContext.Comment;
        }
    }
}
