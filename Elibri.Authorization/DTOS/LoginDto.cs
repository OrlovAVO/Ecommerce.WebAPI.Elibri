using System.ComponentModel.DataAnnotations;

namespace Elibri.Authorization.DTOS
{
    /// <summary>
    /// Класс для данных, необходимых для входа пользователя.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Имя пользователя. Поле обязательно для заполнения.
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя. Поле обязательно для заполнения.
        /// Поле имеет атрибут DataType для указания типа данных "Пароль".
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
