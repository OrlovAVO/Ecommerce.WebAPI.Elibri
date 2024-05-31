using Elibri.Api.Web;
using Elibri.EF.DTOS;
using Elibri.Core.Features.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elibri.API.Controllers
{
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        /// <summary>
        /// Получение всех товаров
        /// </summary>
        [HttpGet]
        [Route(Routes.GetAllProductsRoute)]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            var Products = await _ProductService.GetAllAsync();
            if (Products == null)
            {
                return Ok(new List<ProductDTO>());
            }
            return Ok(Products);
        }

        /// <summary>
        /// Получение товара по Id
        /// </summary>
        /// <remarks>
        /// Для получения товара по Id нужно ввести productId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetProductByIdRoute)]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var Product = await _ProductService.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }

        /// <summary>
        /// Получение товара по названию
        /// </summary>
        /// <remarks>
        /// Для получения товара по названию нужно ввести название товара
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetProductByNameRoute)]
        public async Task<ActionResult<ProductDTO>> GetProductByName(string name)
        {
            var product = await _ProductService.GetByNameAsync(name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Создание товара
        /// </summary>
        /// <remarks>
        /// Для создания товара нужны права администратора
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateProductRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO ProductDTO)
        {
            var createdProduct = await _ProductService.CreateAsync(ProductDTO);

            return Ok(createdProduct);
        }

        /// <summary>
        /// Получение товара по categoryId
        /// </summary>
        /// <remarks>
        /// Для получения товара по caregoryId нужно ввести categoryId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetProductByCategoryIdRoute)]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _ProductService.GetProductsByCategoryIdAsync(categoryId);
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

        /// <summary>
        /// Обновление товара 
        /// </summary>
        /// <remarks>
        /// Для обновления товара нужны права администратора и ввести productId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateProductRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ProductDTO ProductDTO)
        {
            var existingDto = await _ProductService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            await _ProductService.UpdateAsync(ProductDTO);
            return Ok("Товар успешно обновлён.");
        }

        /// <summary>
        /// Удаление товара 
        /// </summary>
        /// <remarks>
        /// Для удаления товара нужны права администратора и ввести productId
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteProductRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _ProductService.DeleteAsync(id);
            return Ok("Товар успешно удалён.");
        }





        /*        [HttpGet("{id}/reviews")]
                public async Task<ActionResult<List<ReviewDTO>>> GetReviewsByProductId(int id) // Новое
                {
                    var reviews = await _ProductService.GetReviewsByProductIdAsync(id);
                    if (reviews == null || reviews.Count == 0)
                    {
                        return NotFound();
                    }
                    return Ok(reviews);
                }

                [HttpPost("{id}/reviews")]
                public async Task<ActionResult<ReviewDTO>> AddReview(int id, ReviewDTO reviewDTO) // Новое
                {
                    reviewDTO.ProductId = id;
                    var createdReview = await _ProductService.AddReviewAsync(reviewDTO);
                    return Ok(createdReview);*/
    }
}

