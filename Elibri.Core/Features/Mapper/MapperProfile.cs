using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;

namespace Elibri.Core.Features.Mapper
{
    // Профиль для AutoMapper, содержащий конфигурации маппинга.
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Маппинг между сущностью User и DTO UserDTO, включая обратный маппинг.
            CreateMap<User, UserDTO>().ReverseMap();

            // Маппинг между сущностью Product и DTO ProductDTO, включая обратный маппинг.
            CreateMap<Product, ProductDTO>().ReverseMap();

            // Маппинг между сущностью Category и DTO CategoryDTO, включая обратный маппинг.
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // Маппинг между сущностью Cart и DTO CartDTO, включая обратный маппинг.
            CreateMap<Cart, CartDTO>().ReverseMap();

            // Маппинг между сущностью Review и DTO ReviewDTO, включая обратный маппинг.
            CreateMap<Review, ReviewDTO>().ReverseMap();

            // Маппинг между сущностью Order и DTO OrderDTO.
            // Маппинг для CartItems: преобразование OrderDetails в список CartItemDTO.
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.OrderDetails.Select(od => new CartItemDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.StockQuantity,
                    Image = od.Product.Image
                }).ToList()))
                .ReverseMap();

            // Маппинг между сущностью OrderDetail и DTO OrderDetailDTO.
            // Маппинг для CartItems: преобразование OrderDetails в список CartItemDTO.
            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Order.OrderDetails.Select(od => new CartItemDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.StockQuantity,
                    Image = od.Product.Image
                }).ToList()))
                .ReverseMap();

            // Маппинг между сущностью Product и DTO ProductWithRelatedDTO, включая обратный маппинг.
            CreateMap<Product, ProductWithRelatedDTO>().ReverseMap();
        }
    }
}
