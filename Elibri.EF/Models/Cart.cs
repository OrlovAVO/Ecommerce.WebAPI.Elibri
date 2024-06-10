using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель корзины.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Идентификатор корзины.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "CartId is required.")]
        public int CartId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }

        /// <summary>
        /// Связанный пользователь.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Элементы корзины.
        /// </summary>
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
