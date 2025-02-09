using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gettit.Service.Comment
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository commentRepository;

        private readonly GettitThreadRepository threadRepository;

        public CommentService(
            CommentRepository commentRepository, 
            GettitThreadRepository threadRepository)
        {
            this.commentRepository = commentRepository;
            this.threadRepository = threadRepository;
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

        public Task<Data.Models.Comment> InternalCreateAsync(Data.Models.Comment model)
        {
            throw new NotImplementedException();
        }

        public Task<Data.Models.Comment> InternalGetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CommentServiceModel> UpdateAsync(string id, CommentServiceModel model)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Data.Models.Comment> InternalGetAll()
        {
            return commentRepository.GetAll()
                .Include(c => c.Attachments)
                .Include(t => t.Reactions)
                .Include(c => c.Replies)
                .Include(t => t.CreatedBy)
                .Include(t => t.UpdatedBy)
                .Include(t => t.DeletedBy);
        }
    }
}
