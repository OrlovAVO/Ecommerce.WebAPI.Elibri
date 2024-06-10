using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для корзины покупок.
    /// </summary>
    public class CartDTO
    {
        /// <summary>
        /// Уникальный идентификатор корзины.
        /// </summary>
        [Key]
        [Required]
        public int CartId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит корзина.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Список элементов в корзине.
        /// </summary>
        public List<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();
    }

}
