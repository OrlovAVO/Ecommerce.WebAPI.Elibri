using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Context;
using Elibri.Repositories.ReviewRepo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elibri.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly Context.Context _context;

        public ReviewService(Context.Context context)
        {
            _context = context;
        }

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

        public async Task DeleteReview(int reviewId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

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

        public async Task<double> GetProductRating(int productId)
        {
            var reviews = _context.Reviews.Where(r => r.ProductId == productId);
            if (reviews.Any())
            {
                return reviews.Average(r => r.Rating);
            }
            return 0;
        }

        public async Task<int> GetReviewCount(int productId)
        {
            return _context.Reviews.Count(r => r.ProductId == productId);
        }
    }
}
