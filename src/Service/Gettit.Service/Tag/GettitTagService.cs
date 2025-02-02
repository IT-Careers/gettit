using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;

namespace Gettit.Service.Tag
{
    public class GettitTagService : IGettitTagService
    {
        private readonly GettitTagRepository gettitTagRepository;

        public GettitTagService(GettitTagRepository gettitTagRepository)
        {
            this.gettitTagRepository = gettitTagRepository;
        }

        public async Task<GettitTagServiceModel> CreateAsync(GettitTagServiceModel model)
        {
            return (await this.gettitTagRepository.CreateAsync(model.ToEntity())).ToModel();
        }

        public async Task<GettitTag> InternalCreateAsync(GettitTag entity)
        {
            return await this.gettitTagRepository.CreateAsync(entity);
        }

        public Task<GettitTagServiceModel> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GettitTagServiceModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GettitTagServiceModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GettitTagServiceModel> UpdateAsync(string id, GettitTagServiceModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GettitTag> InternalGetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
