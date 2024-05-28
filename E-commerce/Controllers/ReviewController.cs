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

    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly Context _context;

        public ReviewsController(IReviewService reviewService, Context context)
        {
            _reviewService = reviewService;
            _context = context;
        }


        /// <summary>
        /// Создание/обновление отзыва
        /// </summary>
        /// <remarks>
        /// Для создания отзыва нужно авторизироваться
        /// </remarks>
        [HttpPost]
        [Route(Routes.AddOrUpdateReviewRoute)]
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


        /// <summary>
        /// Удаление отзыва 
        /// </summary>
        /// <remarks>
        ///Для удаления отзыва нужно авторизироваться и ввести reviewId
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteReviewRoute)]
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


        /// <summary>
        /// Получение отзыва по productId
        /// </summary>
        /// <remarks>
        ///Для получения отзыва нужно ввести productId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetReviewsByProductRoute)]
        public IActionResult GetReviewsByProduct(int productId)
        {
            var reviews = _reviewService.GetReviewsByProduct(productId);
            return Ok(reviews);
        }


        /// <summary>
        /// Получение рейтинга товара
        /// </summary>
        /// <remarks>
        /// Для получение рейтинга товара нужно ввести productId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetProductRatingRoute)]
        public IActionResult GetProductRating(int productId)
        {
            var rating = _reviewService.GetProductRating(productId);
            return Ok(rating);
        }


        /// <summary>
        /// Получение количество отзывов товара
        /// </summary>
        /// <remarks>
        /// Для получение всех отзывов товара нужно ввести productId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetReviewCountRoute)]
        public IActionResult GetReviewCount(int productId)
        {
            var count = _reviewService.GetReviewCount(productId);
            return Ok(count);
        }
    }
}
