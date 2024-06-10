using Elibri.EF.Models;

namespace Elibri.Core.Repository.UserRepo
{
    // Интерфейс IUserRepository, определяющий методы для работы с пользователями.
    public interface IUserRepository
    {
        // Получение всех пользователей асинхронно.
        Task<List<User>> GetAllAsync();

        // Получение пользователя по идентификатору асинхронно.
        Task<User> GetByIdAsync(string id);

        // Получение пользователя по имени пользователя асинхронно.
        Task<User> GetByUsernameAsync(string username);

        // Создание нового пользователя асинхронно.
        Task<User> CreateAsync(User entity);

        // Обновление информации о пользователе асинхронно.
        Task UpdateAsync(User entity);

        // Удаление пользователя по идентификатору асинхронно.
        Task DeleteAsync(int id);

        // Получение пользователя по адресу электронной почты асинхронно.
        Task<User> GetByEmailAsync(string email);
    }
}
