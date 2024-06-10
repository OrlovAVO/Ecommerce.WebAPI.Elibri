using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.DTOS;
using Elibri.EF.Models;

namespace Elibri.Core.Features.CategoryServices
{
    // Сервис для работы с категориями.
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Получает все категории асинхронно.
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Image = c.Image,
            }).ToList();
        }

        // Получает категорию по идентификатору асинхронно.
        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Image = category.Image,
            };
        }

        // Создает новую категорию асинхронно.
        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            if (string.IsNullOrEmpty(categoryDTO.Image))
            {
                throw new ArgumentException("Необходимо указать изображение для категории.");
            }

            var category = new Category
            {
                Name = categoryDTO.Name,
                Image = categoryDTO.Image
            };

            var createdCategory = await _categoryRepository.CreateAsync(category);
            return new CategoryDTO
            {
                CategoryId = createdCategory.CategoryId,
                Name = createdCategory.Name,
                Image = createdCategory.Image
            };
        }

        // Обновляет информацию о категории асинхронно.
        public async Task UpdateAsync(CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDTO.CategoryId);
            if (category == null) return;

            category.Name = categoryDTO.Name;

            await _categoryRepository.UpdateAsync(category);
        }

        // Удаляет категорию по идентификатору асинхронно.
        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
