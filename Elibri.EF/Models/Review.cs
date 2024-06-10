using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель отзыва.
    /// </summary>
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        /// <summary>
        /// Идентификатор продукта, к которому относится отзыв.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Продукт, к которому относится отзыв.
        /// </summary>
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        /// <summary>
        /// Идентификатор пользователя, оставившего отзыв.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Пользователь, оставивший отзыв.
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// Имя пользователя, оставившего отзыв.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ReviewerName { get; set; }

        /// <summary>
        /// Текст отзыва.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; }

        /// <summary>
        /// Рейтинг, оставленный пользователем.
        /// </summary>
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
