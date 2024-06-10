namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для продукта с связанными продуктами.
    /// </summary>
    public class ProductWithRelatedDTO
    {
        /// <summary>
        /// Связанные продукты.
        /// </summary>
        public List<ProductDTO> RelatedProducts { get; set; }
    }
}
