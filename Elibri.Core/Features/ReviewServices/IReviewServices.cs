using Elibri.EF.DTOS;
using Elibri.Core.Features.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Features.ReviewServices
{
    public interface IReviewService
    {
        Task AddOrUpdateReview(ReviewDTO reviewDto);
        Task DeleteReview(int reviewId);
        Task<IEnumerable<ReviewDTO>> GetReviewsByProduct(int productId);
        Task<double> GetProductRating(int productId);
        Task<int> GetReviewCount(int productId);
    }

}
