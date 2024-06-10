using System.ComponentModel.DataAnnotations;

namespace Elibri.Authorization.DTOS
{
    /// <summary>
    /// Класс для данных, необходимых для смены пароля.
    /// </summary>
    public class ChangePasswordDTO
    {
        /// <summary>
        /// Текущий пароль пользователя. Поле обязательно для заполнения.
        /// </summary>
        [Required(ErrorMessage = "Текущий пароль обязателен")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль пользователя. Поле обязательно для заполнения и должно быть длиной от 6 до 100 символов.
        /// </summary>
        [Required(ErrorMessage = "Новый пароль обязателен")]
        [StringLength(100, ErrorMessage = "Пароль должен быть не менее {6} символов.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Подтверждение нового пароля. Должно совпадать с полем NewPassword.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

}
