﻿using Elibri.EF.DTOS;
using Elibri.EF.Models;

namespace Elibri.Core.Features.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly Context _context;

        public ReviewService(Context context)
        {
            _context = context;
        }

        // Добавляет или обновляет отзыв.
        public async Task AddOrUpdateReview(ReviewDTO reviewDto)
        {
            var existingReview = _context.Reviews
                .FirstOrDefault(r => r.ProductId == reviewDto.ProductId && r.UserId == reviewDto.UserId);

            if (existingReview != null)
            {
                existingReview.Comment = reviewDto.Comment;
                existingReview.Rating = reviewDto.Rating;
            }
            else
            {
                var newReview = new Review
                {
                    ProductId = reviewDto.ProductId,
                    UserId = reviewDto.UserId,
                    Comment = reviewDto.Comment,
                    Rating = reviewDto.Rating,
                    ReviewerName = reviewDto.ReviewerName
                };
                _context.Reviews.Add(newReview);
            }

            await _context.SaveChangesAsync();
        }

        // Удаляет отзыв по идентификатору.
        public async Task DeleteReview(int reviewId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        // Получает отзывы по идентификатору продукта.
        public async Task<IEnumerable<ReviewDTO>> GetReviewsByProduct(int productId)
        {
            return _context.Reviews
                .Where(r => r.ProductId == productId)
                .Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    ProductId = r.ProductId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    ReviewerName = r.ReviewerName
                }).ToList();
        }

        // Получает рейтинг продукта по идентификатору.
        public async Task<double> GetProductRating(int productId)
        {
            var reviews = _context.Reviews.Where(r => r.ProductId == productId);
            if (reviews.Any())
            {
                return reviews.Average(r => r.Rating);
            }
            return 0;
        }

        // Получает количество отзывов для продукта по идентификатору.
        public async Task<int> GetReviewCount(int productId)
        {
            return _context.Reviews.Count(r => r.ProductId == productId);
        }
    }
}
