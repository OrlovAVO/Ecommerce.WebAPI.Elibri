using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для пользователя.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        [Required(ErrorMessage = "Электронная почта обязательна.")]
        [EmailAddress(ErrorMessage = "Неправильная электронная почта")]
        public string Email { get; set; }
    }
}
