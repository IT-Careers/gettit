using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
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

        public static CommentServiceModel ToModel(this Comment entity)
        {
            return new CommentServiceModel
            {
                Id = entity.Id,
                Content = entity.Content,
                Attachments = entity.Attachments?.Select(attachment => attachment.ToModel()).ToList(),
                Reactions = entity.Reactions?.Select(reaction => reaction.ToModel()).ToList(),
                Replies = entity.Replies?.Select(reply => reply.ToModel()).ToList(),
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
