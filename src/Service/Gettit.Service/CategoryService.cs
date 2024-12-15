using Gettit.Data.Models;
using Gettit.Data.Repositories;
using Gettit.Service.Mappings;
using Gettit.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryServiceModel> CreateAsync(CategoryServiceModel model)
        {
            Category category = model.ToEntity();

            await this.categoryRepository.CreateAsync(category);

            return category.ToModel();
        }

        public async Task<CategoryServiceModel> DeleteAsync(string id)
        {
            Category category = await this.categoryRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NullReferenceException($"No category found with id - {id}.");
            }

            await this.categoryRepository.DeleteAsync(category);

            return category.ToModel();
        }

        public IQueryable<CategoryServiceModel> GetAll()
        {
            return this.categoryRepository.GetAll()
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.DeletedBy)
                .Select(c => c.ToModel());
        }

        public async Task<CategoryServiceModel> GetByIdAsync(string id)
        {
            return (await this.categoryRepository.GetAll()
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.DeletedBy)
                .SingleOrDefaultAsync(c => c.Id == id))?.ToModel();
        }

        public async Task<CategoryServiceModel> UpdateAsync(string id, CategoryServiceModel model)
        {
            Category category = await this.categoryRepository.GetAll().SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NullReferenceException($"No category found with id - {id}.");
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.CoverPhoto = model.CoverPhoto != null ? model.CoverPhoto.ToEntity() : category.CoverPhoto;

            await this.categoryRepository.UpdateAsync(category);

            return category.ToModel();
        }
    }
}
