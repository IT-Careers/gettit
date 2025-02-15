using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service.Reaction
{
    public class ReactionService : IReactionService
    {
        private readonly ReactionRepository reactionRepository;

        public ReactionService(ReactionRepository reactionRepository)
        {
            this.reactionRepository = reactionRepository;
        }

        public async Task<ReactionServiceModel> CreateAsync(ReactionServiceModel model)
        {
            Data.Models.Reaction reaction = model.ToEntity();

            reaction = await this.reactionRepository.CreateAsync(reaction);

            return reaction.ToModel();
        }

        public Task<ReactionServiceModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ReactionServiceModel> GetAll()
        {
            return this.InternalGetAll().Select(r => r.ToModel());
        }

        public Task<ReactionServiceModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ReactionServiceModel> UpdateAsync(string id, ReactionServiceModel model)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Data.Models.Reaction> InternalGetAll()
        {
            return this.reactionRepository.GetAll()
                .Include(r => r.Emote)
                .Include(r => r.CreatedBy)
                .Include(r => r.UpdatedBy)
                .Include(r => r.DeletedBy);
        }
    }
}
