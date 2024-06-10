namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель категории.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Наименование категории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Изображение категории.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Продукты в данной категории.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
