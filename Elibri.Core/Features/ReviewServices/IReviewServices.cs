using Elibri.EF.DTOS;

namespace Elibri.Core.Features.ReviewServices
{
    public interface IReviewService
    {
        // Добавляет или обновляет отзыв.
        Task AddOrUpdateReview(ReviewDTO reviewDto);

        // Удаляет отзыв по идентификатору.
        Task DeleteReview(int reviewId);

        // Получает отзывы по идентификатору продукта.
        Task<IEnumerable<ReviewDTO>> GetReviewsByProduct(int productId);

        // Получает рейтинг продукта по идентификатору.
        Task<double> GetProductRating(int productId);

        // Получает количество отзывов для продукта по идентификатору.
        Task<int> GetReviewCount(int productId);
    }
}
