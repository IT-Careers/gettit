using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Gettit.Service.User;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service.Thread
{
    public class GettitThreadService : IGettitThreadService
    {
        private readonly GettitThreadRepository gettitThreadRepository;

        private readonly GettitTagRepository gettitTagRepository;

        private readonly GettitCommunityRepository gettitCommunityRepository;

        private readonly CommentRepository commentRepository;

        private readonly ReactionRepository reactionRepository;

        private readonly UserThreadReactionRepository userThreadReactionRepository;

        private readonly IUserContextService userContextService;

        public GettitThreadService(
            GettitThreadRepository gettitThreadRepository,
            GettitTagRepository gettitTagRepository,
            GettitCommunityRepository gettitCommunityRepository,
            CommentRepository commentRepository,
            IUserContextService userContextService,
            ReactionRepository reactionRepository,
            UserThreadReactionRepository userThreadReactionRepository)
        {
            this.gettitThreadRepository = gettitThreadRepository;
            this.gettitTagRepository = gettitTagRepository;
            this.gettitCommunityRepository = gettitCommunityRepository;
            this.commentRepository = commentRepository;
            this.userContextService = userContextService;
            this.reactionRepository = reactionRepository;
            this.userThreadReactionRepository = userThreadReactionRepository;
        }

        public async Task<GettitThreadServiceModel> CreateAsync(GettitThreadServiceModel model)
        {
            GettitThread gettitThread = model.ToEntity();

            gettitThread.Tags = gettitThread.Tags.Select(async tag => {
                return (await this.gettitTagRepository.CreateAsync(tag));
            }).Select(t => t.Result).ToList();

            gettitThread.Community = await this.gettitCommunityRepository.GetAll()
                .SingleOrDefaultAsync(community => community.Id == model.Community.Id);

            await gettitThreadRepository.CreateAsync(gettitThread);

            return gettitThread.ToModel();
        }

        public async Task<CommentServiceModel> CreateCommentOnThread(CommentServiceModel commentServiceModel, string threadId, string? parentCommentId = null)
        {
            Data.Models.Comment entity = commentServiceModel.ToEntity();

            if (parentCommentId != null)
            {
                Data.Models.Comment? parent = await commentRepository.GetAll()
                    .SingleOrDefaultAsync(c => c.Id == parentCommentId);

                if (parent == null)
                {
                    throw new ArgumentException("Parent comment not found for id - " + parentCommentId);
                }

                entity.Parent = parent;
            }

            entity = await this.commentRepository.CreateAsync(entity);

            GettitThread commentThread = await this.InternalGetByIdAsync(threadId);

            commentThread.Comments.Add(new UserThreadComment
            {
                Comment = entity,
                Thread = commentThread,
                User = (await this.userContextService.GetCurrentUserAsync())
            });

            await this.gettitThreadRepository.UpdateAsync(commentThread);

            return entity.ToModel(CommentMappingsContext.Reaction);
        }

        public async Task<UserThreadReactionServiceModel> CreateReactionOnThread(string threadId, string reactionId)
        {
            GettitThread reactionThread = await this.gettitThreadRepository
                .GetAll()
                .Include(t => t.Reactions)
                    .ThenInclude(utr => utr.Reaction)
                        .ThenInclude(r => r.Emote)
                .Include(t => t.Reactions)
                    .ThenInclude(utr => utr.User)
                .SingleOrDefaultAsync(t => t.Id == threadId);

            GettitUser user = await this.userContextService.GetCurrentUserAsync();

            UserThreadReaction existentReaction = reactionThread.Reactions
                .SingleOrDefault(utr => utr.Reaction.Id == reactionId && utr.User.Id == user.Id);

            if (existentReaction != null)
            {
                await this.userThreadReactionRepository.DeleteAsync(existentReaction);

                return existentReaction.ToModel(UserThreadReactionMappingsContext.User, true);
            }

            Data.Models.Reaction reaction = await reactionRepository.GetAll()
                .SingleOrDefaultAsync(r => r.Id == reactionId);

            var utr = new UserThreadReaction
            {
                Reaction = reaction,
                Thread = reactionThread,
                User = user
            };

            reactionThread.Reactions.Add(utr);

            await this.gettitThreadRepository.UpdateAsync(reactionThread);
            
            return utr.ToModel(UserThreadReactionMappingsContext.User);
        }

        public Task<GettitThreadServiceModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GettitThreadServiceModel> GetAll()
        {
            return this.InternalGetAll()
                .Select(t => t.ToModel());
        }

        public IQueryable<GettitThreadServiceModel> GetAllByCommunityId(string communityId)
        {
            return this.InternalGetAll().Where(t => t.Community.Id == communityId).Select(t => t.ToModel());
        }

        public async Task<GettitThreadServiceModel> GetByIdAsync(string id)
        {
            return (await this.InternalGetAll().SingleOrDefaultAsync(thread => thread.Id == id))?.ToModel();
        }

        public Task<GettitThreadServiceModel> UpdateAsync(string id, GettitThreadServiceModel model)
        {
            throw new NotImplementedException();
        }

        private async Task<GettitThread> InternalGetByIdAsync(string id)
        {
            return await this.InternalGetAll().SingleOrDefaultAsync(thread => thread.Id == id);
        }

        private IQueryable<GettitThread> InternalGetAll()
        {
            return gettitThreadRepository.GetAll()
                .Include(t => t.Tags)
                .Include(t => t.Community)
                    .ThenInclude(c => c.ThumbnailPhoto)
                .Include(t => t.Reactions)
                    .ThenInclude(utr => utr.Reaction)
                        .ThenInclude(r => r.Emote)
                .Include(t => t.Attachments)
                .Include(t => t.Comments)
                .Include(t => t.CreatedBy)
                .Include(t => t.UpdatedBy)
                .Include(t => t.DeletedBy);
        }
    }
}
