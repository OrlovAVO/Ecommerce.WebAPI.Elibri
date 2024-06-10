namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для обзора продукта.
    /// </summary>
    public class ReviewDTO
    {
        /// <summary>
        /// Идентификатор обзора.
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// Идентификатор продукта, к которому относится обзор.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, оставившего обзор.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Имя пользователя, оставившего обзор.
        /// </summary>
        public string ReviewerName { get; set; }

        /// <summary>
        /// Комментарий к обзору.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Рейтинг, оставленный пользователем.
        /// </summary>
        public int Rating { get; set; }
    }
}
