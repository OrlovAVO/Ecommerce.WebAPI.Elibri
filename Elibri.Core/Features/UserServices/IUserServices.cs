using Elibri.EF.DTOS;

namespace Elibri.Core.Features.UserServices
{
    public interface IUserService
    {
        // Получает всех пользователей асинхронно.
        Task<List<UserDTO>> GetAllAsync();

        // Получает пользователя по идентификатору асинхронно.
        Task<UserDTO> GetByIdAsync(string id);

        // Получает пользователя по имени пользователя асинхронно.
        Task<UserDTO> GetByUsernameAsync(string username);

        // Создает нового пользователя асинхронно.
        Task<UserDTO> CreateAsync(UserDTO dto);

        // Обновляет информацию о пользователе асинхронно.
        Task UpdateAsync(UserDTO dto);

        // Удаляет пользователя по идентификатору асинхронно.
        Task DeleteAsync(int id);

        // Получает пользователя по адресу электронной почты асинхронно.
        Task<UserDTO> GetUserByEmail(string email);
    }
}
