namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для деталей заказа.
    /// </summary>
    public class OrderDetailDTO
    {
        /// <summary>
        /// Идентификатор детали заказа.
        /// </summary>
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Элементы корзины.
        /// </summary>
        public List<CartItemDTO> CartItems { get; set; }

        /// <summary>
        /// Общая стоимость.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
