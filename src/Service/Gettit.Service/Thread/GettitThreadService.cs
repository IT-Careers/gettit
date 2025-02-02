using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Community;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Gettit.Service.Tag;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service.Thread
{
    public class GettitThreadService : IGettitThreadService
    {
        private readonly GettitThreadRepository gettitThreadRepository;

        private readonly IGettitTagService gettitTagService;

        private readonly IGettitCommunityService gettitCommunityService;

        public GettitThreadService(GettitThreadRepository gettitThreadRepository, 
            IGettitTagService gettitTagService, 
            IGettitCommunityService gettitCommunityService)
        {
            this.gettitThreadRepository = gettitThreadRepository;
            this.gettitTagService = gettitTagService;
            this.gettitCommunityService = gettitCommunityService;
        }

        public async Task<GettitThreadServiceModel> CreateAsync(GettitThreadServiceModel model)
        {
            GettitThread gettitThread = model.ToEntity();

            gettitThread.Tags = gettitThread.Tags.Select(async tag => {
                return (await this.gettitTagService.InternalCreateAsync(tag));
            }).Select(t => t.Result).ToList();

            gettitThread.Community = await this.gettitCommunityService.InternalGetByIdAsync(model.Community.Id);

            await gettitThreadRepository.CreateAsync(gettitThread);

            return gettitThread.ToModel();
        }

        public Task<GettitThreadServiceModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GettitThreadServiceModel> GetAll()
        {
            return gettitThreadRepository.GetAll()
                .Include(t => t.Tags)
                .Include(t => t.Community)
                .Include(t => t.CreatedBy)
                .Include(t => t.UpdatedBy)
                .Include(t => t.DeletedBy)
                .Select(t => t.ToModel());
        }

        public Task<GettitThreadServiceModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GettitThread> InternalCreateAsync(GettitThread model)
        {
            throw new NotImplementedException();
        }

        public Task<GettitThread> InternalGetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GettitThreadServiceModel> UpdateAsync(string id, GettitThreadServiceModel model)
        {
            throw new NotImplementedException();
        }
    }
}
