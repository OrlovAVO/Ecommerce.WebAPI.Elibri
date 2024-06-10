
namespace Elibri.Core.Features.GenericServices
{
    // Интерфейс для обобщенного сервиса работы с сущностями.
    public interface IGenericService<TDTO>
        where TDTO : class
    {
        // Получает все сущности асинхронно.
        Task<List<TDTO>> GetAllAsync();

        // Получает сущность по идентификатору асинхронно.
        Task<TDTO> GetByIdAsync(int id);

        // Создает новую сущность асинхронно.
        Task<TDTO> CreateAsync(TDTO dto);

        // Обновляет информацию о сущности асинхронно.
        Task UpdateAsync(TDTO dto);

        // Удаляет сущность по идентификатору асинхронно.
        Task DeleteAsync(int id);
    }
}
