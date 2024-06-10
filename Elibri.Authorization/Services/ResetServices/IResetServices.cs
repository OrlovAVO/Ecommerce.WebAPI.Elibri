

namespace Elibri.Authorization.Services.ResetServices
{
    public interface IResetService
    {
        // Метод для сброса пароля по электронной почте.
        // Принимает email пользователя и возвращает кортеж, содержащий:
        // - Success: boolean, указывающий на успех операции.
        // - ErrorMessage: строку, содержащую сообщение об ошибке в случае неудачи.
        Task<(bool Success, string ErrorMessage)> ResetPassword(string email);
    }
}
