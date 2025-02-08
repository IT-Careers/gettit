using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Mappings
{
    public static class ReactionMappings
    {
        public static Reaction ToEntity(this ReactionServiceModel model)
        {
            return new Reaction
            {
                Label = model.Label,
                Emote = model.Emote.ToEntity()
            };
        }

        public static ReactionServiceModel ToModel(this Reaction entity)
        {
            return new ReactionServiceModel
            {
                Id = entity.Id,
                Label = entity.Label,
                Emote = entity.Emote?.ToModel(),
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
