namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для заказа.
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Элементы корзины.
        /// </summary>
        public List<CartItemDTO> CartItems { get; set; }

        /// <summary>
        /// Общая стоимость.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Дата доставки.
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Форматированная дата заказа.
        /// </summary>
        public string OrderDateFormatted => OrderDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));

        /// <summary>
        /// Форматированная дата доставки.
        /// </summary>
        public string DeliveryDateFormatted => DeliveryDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));

        /// <summary>
        /// Форматированный идентификатор заказа.
        /// </summary>
        public string FormattedOrderId => OrderId.HasValue ? $"000000-{OrderId.Value:D4}" : null;
    }
}
