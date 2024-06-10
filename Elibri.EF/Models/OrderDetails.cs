using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель детали заказа.
    /// </summary>
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество товара в заказе.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Общая стоимость детали заказа.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Заказ, к которому относится деталь.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Продукт, связанный с деталью заказа.
        /// </summary>
        public Product Product { get; set; }
    }
}
