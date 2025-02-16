using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class GettitThreadMappings
    {
        public static GettitThread ToEntity(this GettitThreadServiceModel model)
        {
            return new GettitThread
            {
                Title = model.Title,
                Content = model.Content,
                Community = model.Community?.ToEntity(),
                Attachments = model.Attachments.Select(a => a.ToEntity()).ToList(),
                Tags = model.Tags?.Select(tag => tag.ToEntity()).ToList(),
            };
        }

        public static GettitThreadServiceModel ToModel(this GettitThread entity)
        {
            return new GettitThreadServiceModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                Community = entity.Community?.ToModel(),
                Tags = entity.Tags?.Select(tag => tag.ToModel()).ToList(),
                Attachments = entity.Attachments?.Select(attachment => attachment.ToModel()).ToList(),
                Reactions = entity.Reactions?.Select(reaction => reaction.ToModel(UserThreadReactionMappingsContext.Thread)).ToList(),
                Comments = entity.Comments?.Select(comment => comment.ToModel(UserThreadCommentMappingsContext.Thread)).ToList(),
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
