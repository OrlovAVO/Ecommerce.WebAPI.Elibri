using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Elibri.Core.Repository.ProductRepo;

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


        public async Task<List<ProductDTO>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.FilterProductsAsync(maxDeliveryDays, sortByPriceDescending, searchTerm, pageNumber, pageSize);
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductWithRelatedDTO> GetProductWithRelatedAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            int categoryId = product.CategoryId;

            var productsInSameCategory = await _productRepository.GetProductsByCategoryIdAsync(categoryId, 1, 10);

            var relatedProducts = productsInSameCategory
                .Where(p => p.ProductId != productId)
                .Take(8)
                .ToList();

            var productWithRelated = _mapper.Map<ProductWithRelatedDTO>(product);
            productWithRelated.RelatedProducts = _mapper.Map<List<ProductDTO>>(relatedProducts);

            return productWithRelated;
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
    }
}