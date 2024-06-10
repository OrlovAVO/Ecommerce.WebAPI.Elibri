namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для категорий продуктов.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Изображение категории.
        /// </summary>
        public string Image { get; set; }
    }
}
