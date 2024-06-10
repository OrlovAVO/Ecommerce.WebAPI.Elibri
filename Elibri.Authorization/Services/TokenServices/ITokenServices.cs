
namespace Elibri.Authorization.Services.TokenServices
{
    public interface ITokenService
    {
        /// <summary>
        /// Генерирует JWT токен для указанного email.
        /// </summary>
        /// <param name="email">Электронная почта пользователя.</param>
        /// <returns>Сгенерированный JWT токен в виде строки.</returns>
        string GenerateToken(string email);

        /// <summary>
        /// Проверяет, является ли переданный токен действительным.
        /// </summary>
        /// <param name="token">JWT токен в виде строки.</param>
        /// <returns>Значение true, если токен действителен; иначе false.</returns>
        Task<bool> IsValidToken(string token);

        /// <summary>
        /// Извлекает электронную почту из JWT токена.
        /// </summary>
        /// <param name="token">JWT токен в виде строки.</param>
        /// <returns>Электронная почта, извлеченная из токена.</returns>
        string GetEmailFromToken(string token);
    }
}
