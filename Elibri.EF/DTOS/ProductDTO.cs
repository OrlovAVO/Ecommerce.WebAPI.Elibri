namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для продукта.
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Уникальный идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Наименование продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание продукта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена продукта.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Тип продукта.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Цвет продукта.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Гарантия продукта.
        /// </summary>
        public string Warranty { get; set; }

        /// <summary>
        /// Количество продукта на складе.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Уникальный идентификатор категории, к которой принадлежит продукт.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Изображение продукта.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Дополнительные изображения продукта.
        /// </summary>
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }

        /// <summary>
        /// Срок доставки продукта.
        /// </summary>
        public int DeliveryDays { get; set; }
    }
}
