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

        private readonly IUserContextService userContextService;

        public GettitThreadService(
            GettitThreadRepository gettitThreadRepository,
            GettitTagRepository gettitTagRepository,
            GettitCommunityRepository gettitCommunityRepository,
            CommentRepository commentRepository,
            IUserContextService userContextService)
        {
            this.gettitThreadRepository = gettitThreadRepository;
            this.gettitTagRepository = gettitTagRepository;
            this.gettitCommunityRepository = gettitCommunityRepository;
            this.commentRepository = commentRepository;
            this.userContextService = userContextService;
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

        public async Task<CommentServiceModel> CreateCommentOnThread(string threadId, CommentServiceModel commentServiceModel)
        {
            Data.Models.Comment entity = commentServiceModel.ToEntity();

            entity = await this.commentRepository.CreateAsync(entity);

            GettitThread commentThread = await this.InternalGetByIdAsync(threadId);

            commentThread.Comments.Add(new UserThreadComment
            {
                Comment = entity,
                Thread = commentThread,
                User = (await this.userContextService.GetCurrentUserAsync())
            });

            await this.gettitThreadRepository.UpdateAsync(commentThread);

            return entity.ToModel();
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
                .Include(t => t.Reactions)
                .Include(t => t.Comments)
                    .ThenInclude(utc => utc.Comment)
                        .ThenInclude(c => c.Replies)
                .Include(t => t.Comments)
                    .ThenInclude(utc => utc.Comment)
                        .ThenInclude(c => c.Reactions)
                .Include(t => t.CreatedBy)
                .Include(t => t.UpdatedBy)
                .Include(t => t.DeletedBy);
        }
    }
}
