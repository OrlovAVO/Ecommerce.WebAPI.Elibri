using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Elibri.DTOs.DTOS;
using Elibri.Services.AuthServices;
using Elibri.Services.ReviewServices;
using System.Security.Claims;
using Elibri.Models;
using Elibri.Context;
using Microsoft.EntityFrameworkCore;
using API.Web;


namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly Context _context;

        public ReviewsController(IReviewService reviewService, Context context)
        {
            _reviewService = reviewService;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task AddOrUpdateReview(ReviewDTO reviewDto)
        {
            var userId = reviewDto.UserId;
            var userName = reviewDto.ReviewerName;

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.ProductId == reviewDto.ProductId && r.UserId == userId);

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
                    UserId = userId,
                    Comment = reviewDto.Comment,
                    Rating = reviewDto.Rating,
                    ReviewerName = userName
                };
                _context.Reviews.Add(newReview);
            }

            await _context.SaveChangesAsync();
        }

        [HttpDelete("{reviewId}")]
        [Authorize]
        public async Task DeleteReview(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewId == reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }


        [HttpGet("product/{productId}/reviews")]
        public IActionResult GetReviewsByProduct(int productId)
        {
            var reviews = _reviewService.GetReviewsByProduct(productId);
            return Ok(reviews);
        }

        [HttpGet("product/{productId}/rating")]
        public IActionResult GetProductRating(int productId)
        {
            var rating = _reviewService.GetProductRating(productId);
            return Ok(rating);
        }

        [HttpGet("product/{productId}/count")]
        public IActionResult GetReviewCount(int productId)
        {
            var count = _reviewService.GetReviewCount(productId);
            return Ok(count);
        }
    }
}
