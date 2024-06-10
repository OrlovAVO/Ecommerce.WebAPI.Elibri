namespace Elibri.Core.Repository.GenericRepo
{
    // Интерфейс IGenericRepository<T> определяет базовые операции для работы с репозиторием сущностей.
    // Он предоставляет методы для получения, создания, обновления и удаления сущностей типа T.
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();  // Получить все сущности типа T асинхронно.
        Task<T> GetByIdAsync(int id); // Получить сущность типа T по идентификатору асинхронно.
        Task<T> CreateAsync(T entity); // Создать новую сущность типа T асинхронно.
        Task UpdateAsync(T entity); // Обновить существующую сущность типа T асинхронно.
        Task DeleteAsync(int id); // Удалить сущность типа T по идентификатору асинхронно.
    }
}
