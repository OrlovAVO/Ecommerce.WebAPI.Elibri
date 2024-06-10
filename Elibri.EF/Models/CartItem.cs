using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель элемента корзины.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Идентификатор элемента корзины.
        /// </summary>
        [Key]
        public int CartItemId { get; set; }

        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        /// <summary>
        /// Количество продукта.
        /// </summary>
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Связанный продукт.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Идентификатор корзины.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Связанная корзина.
        /// </summary>
        public Cart Cart { get; set; }
    }
}
