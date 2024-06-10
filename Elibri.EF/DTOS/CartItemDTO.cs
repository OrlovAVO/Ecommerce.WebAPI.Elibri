namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для элементов в корзине покупок.
    /// </summary>
    public class CartItemDTO
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество продукта в корзине.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Изображение продукта.
        /// </summary>
        public string Image { get; set; }
    }
}
