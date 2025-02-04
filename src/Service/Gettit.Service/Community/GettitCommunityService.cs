﻿using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Gettit.Service.Tag;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service.Community
{
    public class GettitCommunityService : IGettitCommunityService
    {
        private readonly GettitCommunityRepository gettitCommunityRepository;

        private readonly IGettitTagService gettitTagService;

        public GettitCommunityService(GettitCommunityRepository gettitCommunityRepository, IGettitTagService gettitTagService)
        {
            this.gettitCommunityRepository = gettitCommunityRepository;
            this.gettitTagService = gettitTagService;
        }

        public async Task<GettitCommunityServiceModel> CreateAsync(GettitCommunityServiceModel model)
        {
            GettitCommunity gettitCommunity = model.ToEntity();

            gettitCommunity.Tags = gettitCommunity.Tags.Select(async tag => {
                return (await this.gettitTagService.InternalCreateAsync(tag));
            }).Select(t => t.Result).ToList();

            await gettitCommunityRepository.CreateAsync(gettitCommunity);

            return gettitCommunity.ToModel();
        }

        public Task<GettitCommunity> InternalCreateAsync(GettitCommunity model)
        {
            throw new NotImplementedException();
        }

        public async Task<GettitCommunityServiceModel> DeleteAsync(string id)
        {
            GettitCommunity category = await gettitCommunityRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NullReferenceException($"No category found with id - {id}.");
            }

            await gettitCommunityRepository.DeleteAsync(category);

            return category.ToModel();
        }

        public IQueryable<GettitCommunityServiceModel> GetAll()
        {
            return gettitCommunityRepository.GetAll()
                .Include(c => c.Tags)
                .Include(c => c.ThumbnailPhoto)
                .Include(c => c.BannerPhoto)
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.DeletedBy)
                .Select(c => c.ToModel());
        }

        public async Task<GettitCommunityServiceModel> GetByIdAsync(string id)
        {
            return (await gettitCommunityRepository.GetAll()
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.DeletedBy)
                .SingleOrDefaultAsync(c => c.Id == id))?.ToModel();
        }

        public async Task<GettitCommunity> InternalGetByIdAsync(string id)
        {
            return await gettitCommunityRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<GettitCommunityServiceModel> UpdateAsync(string id, GettitCommunityServiceModel model)
        {
            GettitCommunity category = await gettitCommunityRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NullReferenceException($"No category found with id - {id}.");
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.ThumbnailPhoto = model.ThumbnailPhoto != null ? model.ThumbnailPhoto.ToEntity() : category.ThumbnailPhoto;
            category.BannerPhoto = model.BannerPhoto != null ? model.BannerPhoto.ToEntity() : category.BannerPhoto;

            await gettitCommunityRepository.UpdateAsync(category);

            return category.ToModel();
        }
    }
}
