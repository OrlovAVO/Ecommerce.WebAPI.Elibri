using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Elibri.Core.Repository.ProductRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        public async Task<List<ProductWithRelatedDTO>> GetRelatedProductsAsync(int productId)
        {
            // Получаем товар по его идентификатору
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                // Если товар не найден, возвращаем пустой список
                return new List<ProductWithRelatedDTO>();
            }

            // Получаем идентификатор категории товара
            int categoryId = product.CategoryId;

            // Получаем все товары с тем же идентификатором категории
            var productsInSameCategory = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

            // Исключаем товар с указанным productId из списка
            var relatedProducts = productsInSameCategory
                .Where(p => p.ProductId != productId)
                .Take(10)
                .Select(p => new ProductWithRelatedDTO
                {
                    // Дополнительные поля, если необходимо
                })
                .ToList();

            return relatedProducts;
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

        public async Task<List<ProductDTO>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm)
        {
            var products = await _productRepository.FilterProductsAsync(maxDeliveryDays, sortByPriceDescending, searchTerm);
            return _mapper.Map<List<ProductDTO>>(products);
        }


        public async Task<ProductWithRelatedDTO> GetProductWithRelatedAsync(int productId)
        {
            // Получаем товар по его идентификатору
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                // Если товар не найден, возвращаем null
                return null;
            }

            // Получаем идентификатор категории товара
            int categoryId = product.CategoryId;

            // Получаем все товары с тем же идентификатором категории
            var productsInSameCategory = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

            // Исключаем товар с указанным productId из списка и ограничиваем количество до 10
            var relatedProducts = productsInSameCategory
                .Where(p => p.ProductId != productId)
                .Take(10)
                .ToList();

            // Маппим основную информацию о продукте
            var productWithRelated = _mapper.Map<ProductWithRelatedDTO>(product);
            // Маппим связанные продукты
            productWithRelated.RelatedProducts = _mapper.Map<List<ProductDTO>>(relatedProducts);

            return productWithRelated;
        }
    }
}
