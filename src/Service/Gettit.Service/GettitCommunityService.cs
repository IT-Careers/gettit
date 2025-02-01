using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service
{
    public class GettitCommunityService : IGettitCommunityService
    {
        private readonly GettitCommunityRepository categoryRepository;

        public GettitCommunityService(GettitCommunityRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<GettitCommunityServiceModel> CreateAsync(GettitCommunityServiceModel model)
        {
            GettitCommunity category = model.ToEntity();

            await this.categoryRepository.CreateAsync(category);

            return category.ToModel();
        }

        public async Task<GettitCommunityServiceModel> DeleteAsync(string id)
        {
            GettitCommunity category = await this.categoryRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NullReferenceException($"No category found with id - {id}.");
            }

            await this.categoryRepository.DeleteAsync(category);

            return category.ToModel();
        }

        public IQueryable<GettitCommunityServiceModel> GetAll()
        {
            return this.categoryRepository.GetAll()
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.DeletedBy)
                .Select(c => c.ToModel());
        }

        public async Task<GettitCommunityServiceModel> GetByIdAsync(string id)
        {
            return (await this.categoryRepository.GetAll()
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.DeletedBy)
                .SingleOrDefaultAsync(c => c.Id == id))?.ToModel();
        }

        public async Task<GettitCommunityServiceModel> UpdateAsync(string id, GettitCommunityServiceModel model)
        {
            GettitCommunity category = await this.categoryRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NullReferenceException($"No category found with id - {id}.");
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.ThumbnailPhoto = model.ThumbnailPhoto != null ? model.ThumbnailPhoto.ToEntity() : category.ThumbnailPhoto;
            category.BannerPhoto = model.BannerPhoto != null ? model.BannerPhoto.ToEntity() : category.BannerPhoto;

            await this.categoryRepository.UpdateAsync(category);

            return category.ToModel();
        }
    }
}
