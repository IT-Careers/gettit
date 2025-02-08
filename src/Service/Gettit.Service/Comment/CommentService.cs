using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service.Comment
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<CommentServiceModel> CreateAsync(CommentServiceModel model)
        {
            Data.Models.Comment entity = model.ToEntity();

            return (await this.commentRepository.CreateAsync(entity)).ToModel();
        }

        public Task<CommentServiceModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentServiceModel> GetAll()
        {
            return this.InternalGetAll().Select(c => c.ToModel());
        }

        public Task<CommentServiceModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
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
