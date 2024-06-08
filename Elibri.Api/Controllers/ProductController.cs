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
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _ProductService.GetAllAsync(pageNumber, pageSize);
            return Ok(products);
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
        /// Получение товара по categoryId
        /// </summary>
        /// <remarks>
        /// Для получения товара по caregoryId нужно ввести categoryId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetProductByCategoryIdRoute)]
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetProductsByCategoryId(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _ProductService.GetProductsByCategoryIdAsync(categoryId, pageNumber, pageSize);
            if (result.Items == null || !result.Items.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Фильтрация товаров
        /// </summary>
        /// <remarks>
        /// Фильтрация товаров по максимальному времени доставки, сортировке по цене и поиску по названию
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetFilteredProductsRoute)]
        public async Task<ActionResult<PagedResult<ProductDTO>>> FilterProducts(
            [FromQuery] int? maxDeliveryDays,
            [FromQuery] bool sortByPriceDescending,
            [FromQuery] int? categoryId = null,
            [FromQuery] string searchTerm = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 7)
        {
            if (!categoryId.HasValue && !maxDeliveryDays.HasValue && string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Должен быть указан хотя бы один параметр фильтрации.");
            }

            var pagedResult = await _ProductService.FilterProductsAsync(categoryId, maxDeliveryDays, sortByPriceDescending, searchTerm, pageNumber, pageSize);

            if (pagedResult.Items == null || !pagedResult.Items.Any())
            {
                return NotFound("К сожалению, данные товары не найдены.");
            }

            return Ok(pagedResult);
        }


        /// <summary>
        /// Получение похожих товаров 
        /// </summary>
        /// <remarks>
        /// Для получения похожих товаров нужно указать productId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetProductWithRelatedRoute)]
        public async Task<ActionResult<ProductWithRelatedDTO>> GetProductWithRelated(int productId)
        {
            var productWithRelated = await _ProductService.GetProductWithRelatedAsync(productId);
            if (productWithRelated == null)
            {
                return NotFound("Продукт не найден.");
            }
            return Ok(productWithRelated);
        }

        /// <summary>
        /// Создание товара
        /// </summary>
        /// <remarks>
        /// Для создания товара нужны права администратора
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateProductRoute)]
/*        [Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO ProductDTO)
        {
            var createdProduct = await _ProductService.CreateAsync(ProductDTO);

            return Ok(createdProduct);
        }


        /// <summary>
        /// Обновление товара 
        /// </summary>
        /// <remarks>
        /// Для обновления товара нужны права администратора и ввести productId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateProductRoute)]
/*        [Authorize(Roles = "Admin")]*/
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
/*        [Authorize(Roles = "Admin")]*/
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

