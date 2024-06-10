using System.ComponentModel.DataAnnotations;

namespace Elibri.Authorization.DTOS
{
    /// <summary>
    /// Класс для данных, необходимых для сброса пароля.
    /// </summary>
    public class ResetPasswordDTO
    {
        /// <summary>
        /// Адрес электронной почты. Поле обязательно для заполнения.
        /// Включает проверку формата электронной почты.
        /// </summary>
        [Required(ErrorMessage = "Обязательно укажите электронную почту")]
        [EmailAddress(ErrorMessage = "Неправильный адрес электронной почты")]
        public string Email { get; set; }
    }

}
