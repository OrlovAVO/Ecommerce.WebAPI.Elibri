using System.ComponentModel.DataAnnotations;

namespace Elibri.Authorization.DTOS
{
    /// <summary>
    /// Класс для данных, необходимых для регистрации нового пользователя.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Имя пользователя. Поле обязательно для заполнения.
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string UserName { get; set; }

        /// <summary>
        /// Адрес электронной почты. Поле обязательно для заполнения.
        /// Включает проверку формата электронной почты.
        /// </summary>
        [Required(ErrorMessage = "Адрес электронной почты обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя. Поле обязательно для заполнения.
        /// Поле имеет атрибут DataType для указания типа данных "Пароль".
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля. Поле обязательно для заполнения.
        /// Включает сравнение с полем "Password" для проверки совпадения паролей.
        /// </summary>
        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

}
