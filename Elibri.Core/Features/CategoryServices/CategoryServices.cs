﻿using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.DTOS;
using Elibri.Core.Repository.UserRepo;
using Elibri.Core.Features.GenericServices;
using Elibri.Core.Features.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Features.CategoryServices
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

            };
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            if (string.IsNullOrEmpty(categoryDTO.Image))
            {
                throw new ArgumentException("Необходимо указать изображение для категории.");
            }

            var category = new Category
            {
                Name = categoryDTO.Name,
                Image = categoryDTO.Image  // Установка значения для свойства Image
            };

            var createdCategory = await _categoryRepository.CreateAsync(category);
            return new CategoryDTO
            {
                CategoryId = createdCategory.CategoryId,
                Name = createdCategory.Name,
                Image = createdCategory.Image  // Возможно, вы захотите возвращать ссылку на изображение
            };
        }

        public async Task UpdateAsync(CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDTO.CategoryId);
            if (category == null) return;

            category.Name = categoryDTO.Name;


            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}