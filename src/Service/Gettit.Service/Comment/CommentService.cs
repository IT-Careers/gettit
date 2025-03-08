using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Gettit.Service.User;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service.Comment
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository commentRepository;

        private readonly GettitThreadRepository threadRepository;

        private readonly ReactionRepository reactionRepository;

        private readonly UserCommentReactionRepository userCommentReactionRepository;

        private readonly IUserContextService userContextService;

        public CommentService(
            CommentRepository commentRepository,
            GettitThreadRepository threadRepository,
            ReactionRepository reactionRepository,
            IUserContextService userContextService,
            UserCommentReactionRepository userCommentReactionRepository)
        {
            this.commentRepository = commentRepository;
            this.threadRepository = threadRepository;
            this.reactionRepository = reactionRepository;
            this.userContextService = userContextService;
            this.userCommentReactionRepository = userCommentReactionRepository;
        }

        public async Task<CommentServiceModel> CreateAsync(CommentServiceModel model)
        {
            Data.Models.Comment entity = model.ToEntity();

            return (await this.commentRepository.CreateAsync(entity)).ToModel(CommentMappingsContext.Reaction);
        }

        public Task<CommentServiceModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentServiceModel> GetAll()
        {
            return this.InternalGetAll().Select(c => c.ToModel(CommentMappingsContext.User));
        }

        public IQueryable<CommentServiceModel> GetAllByParentId(string parentId)
        {
            return this.InternalGetAll()
                .Where(c => c.Parent.Id == parentId)
                .Select(c => c.ToModel(CommentMappingsContext.Parent));
        }

        public Task<CommentServiceModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<CommentServiceModel>> GetAllByThreadId(string threadId)
        {
            GettitThread thread = await this.threadRepository.GetAll()
                .Include(t => t.Comments)
                    .ThenInclude(c => c.Comment)
                        .ThenInclude(c => c.Parent)
                .Include(t => t.Comments)
                    .ThenInclude(c => c.Comment)
                        .ThenInclude(c => c.Replies)
                .Include(t => t.Comments)
                    .ThenInclude(c => c.Comment)
                        .ThenInclude(c => c.Reactions)
                            .ThenInclude(ucr => ucr.Reaction)
                                .ThenInclude(r => r.Emote)
                .SingleOrDefaultAsync(t => t.Id == threadId);

            if(thread == null)
            {
                throw new ArgumentException("No thread exists with id - " + threadId);
            }

            return thread.Comments
                .Where(c => c.Comment.Parent == null)
                .Select(c => c.Comment.ToModel(CommentMappingsContext.Parent))
                .AsQueryable();
        }

        public Task<CommentServiceModel> UpdateAsync(string id, CommentServiceModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserCommentReactionServiceModel> CreateReactionOnComment(string commentId, string reactionId)
        {
            Data.Models.Comment reactionComment = await this.InternalGetByIdAsync(commentId);
            GettitUser user = await this.userContextService.GetCurrentUserAsync();

            UserCommentReaction existentReaction = reactionComment.Reactions
                .SingleOrDefault(utr => utr.Reaction.Id == reactionId && utr.User.Id == user.Id);

            if (existentReaction != null)
            {
                await this.userCommentReactionRepository.DeleteAsync(existentReaction);

                return existentReaction.ToModel(UserCommentReactionMappingsContext.User, true);
            }


            Data.Models.Reaction reaction = await reactionRepository.GetAll()
                    .SingleOrDefaultAsync(r => r.Id == reactionId);

            var ucr = new UserCommentReaction
            {
                Reaction = reaction,
                Comment = reactionComment,
                User = user
            };

            reactionComment.Reactions.Add(ucr);

            await this.commentRepository.UpdateAsync(reactionComment);

            return ucr.ToModel(UserCommentReactionMappingsContext.User);
        }

        private async Task<Data.Models.Comment> InternalGetByIdAsync(string id)
        {
            return await this.InternalGetAll().SingleOrDefaultAsync(c => c.Id == id);
        }

        private IQueryable<Data.Models.Comment> InternalGetAll()
        {
            return commentRepository.GetAll()
                .Include(c => c.Attachments)
                .Include(t => t.Reactions)
                    .ThenInclude(ucr => ucr.Reaction)
                        .ThenInclude(r => r.Emote)
                .Include(t => t.Reactions)
                    .ThenInclude(ucr => ucr.User)
                .Include(c => c.Replies)
                .Include(t => t.CreatedBy)
                .Include(t => t.UpdatedBy)
                .Include(t => t.DeletedBy);
        }
    }
}
