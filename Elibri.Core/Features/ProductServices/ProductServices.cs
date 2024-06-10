using AutoMapper;
using Elibri.Core.Repository.ProductRepo;
using Elibri.EF.DTOS;
using Elibri.EF.Models;

namespace Elibri.Core.Features.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // Получает все продукты асинхронно с разбиением на страницы.
        public async Task<PagedResult<ProductDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var products = await _productRepository.GetAllAsync(pageNumber, pageSize);
            var totalItems = await _productRepository.CountAsync();
            return new PagedResult<ProductDTO>
            {
                Items = _mapper.Map<List<ProductDTO>>(products),
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Получает продукт по идентификатору асинхронно.
        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        // Создает продукт асинхронно.
        public async Task<ProductDTO> CreateAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }

        // Получает все продукты по идентификатору категории асинхронно с разбиением на страницы.
        public async Task<PagedResult<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId, pageNumber, pageSize);
            var totalItems = await _productRepository.CountByCategoryAsync(categoryId);
            return new PagedResult<ProductDTO>
            {
                Items = _mapper.Map<List<ProductDTO>>(products),
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Фильтрует продукты асинхронно по различным параметрам с разбиением на страницы.
        public async Task<PagedResult<ProductDTO>> FilterProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string sortOrder,
            string searchTerm,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var (products, totalItems) = await _productRepository.FilterProductsAsync(
                categoryId, maxDeliveryDays, sortOrder, searchTerm, pageNumber, pageSize);

            var productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return new PagedResult<ProductDTO>
            {
                Items = productDTOs,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Получает продукт с его связанными элементами асинхронно по идентификатору.
        public async Task<ProductWithRelatedDTO> GetProductWithRelatedAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            int categoryId = product.CategoryId;

            var productsInSameCategory = await _productRepository.GetProductsByCategoryIdAsync(categoryId, 1, 11);

            var relatedProducts = productsInSameCategory
                .Where(p => p.ProductId != productId)
                .Take(10)
                .ToList();

            var productWithRelated = _mapper.Map<ProductWithRelatedDTO>(product);
            productWithRelated.RelatedProducts = _mapper.Map<List<ProductDTO>>(relatedProducts);

            return productWithRelated;
        }

        // Обновляет информацию о продукте асинхронно.
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

        // Удаляет продукт асинхронно по идентификатору.
        public async Task DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new ArgumentException($"Товар с id {id} не найден.");
            }

            await _productRepository.DeleteAsync(existingProduct.ProductId);
        }

        // Получает продукт по имени асинхронно.
        public async Task<ProductDTO> GetByNameAsync(string name)
        {
            var product = await _productRepository.GetByNameAsync(name);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
