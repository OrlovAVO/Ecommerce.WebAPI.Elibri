namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для создания элемента корзины.
    /// </summary>
    public class CreateCartItemDTO
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество продукта.
        /// </summary>
        public int Quantity { get; set; }
    }
}
