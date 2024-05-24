using AutoMapper;
using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using Elibri.Repositories.UserRepo;
using Elibri.Services.GenericServices;
using Elibri.Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };
            var createdCategory = await _categoryRepository.CreateAsync(category);
            return new CategoryDTO
            {
                CategoryId = createdCategory.CategoryId,
                Name = createdCategory.Name,
                Description = createdCategory.Description
            };
        }

        public async Task UpdateAsync(CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDTO.CategoryId);
            if (category == null) return;

            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}