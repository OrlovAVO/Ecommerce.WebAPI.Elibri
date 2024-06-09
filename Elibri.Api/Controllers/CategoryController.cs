using Elibri.Api.Web;
using Elibri.EF.DTOS;
using Elibri.Core.Features.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Elibri.Api.Controllers
{
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <remarks>
        /// Для получения категорий нужно авторизироваться
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetCategoriesRoute)]
/*        [Authorize(Roles = "User")]*/
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return Ok(new List<CategoryDTO>());
            }
            return Ok(categories);
        }

        /// <summary>
        /// Получение категории по CategoryId
        /// </summary>
        /// <remarks>
        /// Для получения категории нужно авторизироваться и ввести CategoryId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetCategoryByIdRoute)]
/*        [Authorize(Roles = "User")]*/
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Создание категории
        /// </summary>
        /// <remarks>
        /// Для получения категории нужно авторизироваться админом и ввести CategoryId
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateCategoryRoute)]
/*        [Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO)
        {
            var createdCategory = await _categoryService.CreateAsync(categoryDTO);
            return Ok(createdCategory);
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <remarks>
        /// Для обновления категории нужно авторизироваться админом и ввести CategoryId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateCategoryRoute)]
/*        [Authorize(Roles = "Admin")]*/
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

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <remarks>
        /// Для удаления категории нужно авторизироваться админом и ввести CategoryId
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteCategoryRoute)]
/*        [Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok("Категория успешно удалена.");
        }
    }
}



//РЕАЛИЗОВАТЬ ПО НАЗВАНИЮ ВЫТЯГИВАНИЕ