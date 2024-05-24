using System.ComponentModel.DataAnnotations;

namespace Elibri.DTOs.DTOS 
{ 

    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "Обязательно укажите электронную почту")]
        [EmailAddress(ErrorMessage = "Неправильный адрес электронной почты")]
        public string Email { get; set; }

    }
}