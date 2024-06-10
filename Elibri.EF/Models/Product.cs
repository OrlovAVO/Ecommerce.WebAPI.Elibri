using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель продукта.
    /// </summary>
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Название продукта.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Описание продукта.
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; }

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
        /// Цена продукта.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Количество продукта на складе.
        /// </summary>
        [Required]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Идентификатор категории продукта.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Путь к изображению продукта.
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
        /// Категория, к которой принадлежит продукт.
        /// </summary>
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        /// <summary>
        /// Детали заказа, связанные с продуктом.
        /// </summary>
        public ICollection<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Отзывы, связанные с продуктом.
        /// </summary>
        public ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Время доставки продукта.
        /// </summary>
        [Required]
        public int DeliveryDays { get; set; }
    }
}
