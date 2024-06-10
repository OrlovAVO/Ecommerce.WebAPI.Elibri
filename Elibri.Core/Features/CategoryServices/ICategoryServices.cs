using Elibri.EF.DTOS;

namespace Elibri.Core.Features.CategoryServices
{
    // Интерфейс для сервиса работы с категориями.
    public interface ICategoryService
    {
        // Получает все категории асинхронно.
        Task<List<CategoryDTO>> GetAllAsync();

        // Получает категорию по идентификатору асинхронно.
        Task<CategoryDTO> GetByIdAsync(int id);

        // Создает новую категорию асинхронно.
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);

        // Обновляет информацию о категории асинхронно.
        Task UpdateAsync(CategoryDTO categoryDTO);

        // Удаляет категорию по идентификатору асинхронно.
        Task DeleteAsync(int id);
    }
}
