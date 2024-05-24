using System.ComponentModel.DataAnnotations;

namespace Elibri.DTOs.DTOS 
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Текущий пароль обязателен")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Новый пароль обязателен")]
        [StringLength(100, ErrorMessage = "Пароль должен быть не менее {6} символов.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

}