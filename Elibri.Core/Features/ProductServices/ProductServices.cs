using AutoMapper;
using Elibri.EF.Models;
using Elibri.Core.Repository.ProductRepo;
using Elibri.EF.DTOS;
using Elibri.Core.Repository.GenericRepo;

namespace Elibri.Core.Features.ProductServices
{


    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        /*        private readonly IGenericRepository<Review> _reviewRepository;*/
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            /*            _reviewRepository = reviewRepository;*/
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productDTO.ProductId);
            if (existingProduct == null)
            {
                throw new ArgumentException($"Товар с id {productDTO.ProductId} не найден.");
            }

            _mapper.Map(productDTO, existingProduct);
            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new ArgumentException($"Товар с id {id} не найден.");
            }

            await _productRepository.DeleteAsync(existingProduct.ProductId);
        }

        public async Task<ProductDTO> GetByNameAsync(string name)
        {
            var product = await _productRepository.GetByNameAsync(name);
            return _mapper.Map<ProductDTO>(product);
        }

        /*       public async Task<List<ReviewDTO>> GetReviewsByProductIdAsync(int productId) // Новое
               {
                   var reviews = await _reviewRepository.FindByConditionAsync(r => r.ProductId == productId);
                   return _mapper.Map<List<ReviewDTO>>(reviews);
               }

               public async Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDTO) // Новое
               {
                   var review = _mapper.Map<Review>(reviewDTO);
                   review = await _reviewRepository.CreateAsync(review);
                   return _mapper.Map<ReviewDTO>(review);
               }*/
    }

}
