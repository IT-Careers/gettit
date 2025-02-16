using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public enum CommentMappingsContext
    {
        Reaction,
        Parent,
        Reply,
        User
    }

    public static class CommentMappings
    {
        public static Comment ToEntity(this CommentServiceModel model)
        {
            return new Comment
            {
                Content = model.Content,
                Attachments = model.Attachments?.Select(attachment => attachment.ToEntity()).ToList(),
            };
        }

        public static CommentServiceModel ToModel(this Comment entity, CommentMappingsContext context)
        {
            return new CommentServiceModel
            {
                Id = entity.Id,
                Content = entity.Content,
                Attachments = entity.Attachments?.Select(attachment => attachment.ToModel()).ToList(),
                Reactions = ShouldMapReactions(context) ? entity.Reactions?.Select(reaction => reaction.ToModel(UserCommentReactionMappingsContext.Comment)).ToList() : null,
                Replies = ShouldMapReplies(context) ? entity.Replies?.Select(reply => reply.ToModel(CommentMappingsContext.Parent)).ToList() : null,
                Parent = ShouldMapParent(context) ? entity.Parent?.ToModel(CommentMappingsContext.Reply) : null,
                CreatedOn = entity.CreatedOn,
                UpdatedOn = entity.UpdatedOn,
                DeletedOn = entity.DeletedOn,
                CreatedBy = ShouldMapUser(context) ? entity.CreatedBy.ToModel() : null,
                UpdatedBy = ShouldMapUser(context) ? entity.UpdatedBy?.ToModel() : null,
                DeletedBy = ShouldMapUser(context) ? entity.DeletedBy?.ToModel() : null
            };
        }
        private static bool ShouldMapReactions(CommentMappingsContext context)
        {
            return context == CommentMappingsContext.Parent 
                || context == CommentMappingsContext.Reply 
                || context == CommentMappingsContext.User;
        }

        private static bool ShouldMapReplies(CommentMappingsContext context)
        {
            return context == CommentMappingsContext.Parent 
                || context == CommentMappingsContext.Reaction 
                || context == CommentMappingsContext.User;
        }

        private static bool ShouldMapParent(CommentMappingsContext context)
        {
            return context == CommentMappingsContext.Reaction 
                || context == CommentMappingsContext.Reply
                || context == CommentMappingsContext.User;
        }

        private static bool ShouldMapUser(CommentMappingsContext context)
        {
            return context == CommentMappingsContext.Reaction
                || context == CommentMappingsContext.Parent
                || context == CommentMappingsContext.Reply;
        }
    }
}
