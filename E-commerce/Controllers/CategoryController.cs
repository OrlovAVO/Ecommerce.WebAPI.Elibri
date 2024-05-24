using Elibri.DTOs.DTOS;
using Elibri.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return Ok(new List<CategoryDTO>());
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO)
        {
            var createdCategory = await _categoryService.CreateAsync(categoryDTO);
            return Ok(createdCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
        {
            var existingDto = await _categoryService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            await _categoryService.UpdateAsync(categoryDTO);
            return Ok("Категория успешно обновлена.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok("Категория успешно удалена.");
        }
    }
}



//РЕАЛИЗОВАТЬ ПО НАЗВАНИЮ ВЫТЯГИВАНИЕ