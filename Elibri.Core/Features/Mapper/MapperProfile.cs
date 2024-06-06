using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using System.Linq;

namespace Elibri.Core.Features.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.OrderDetails.Select(od => new CartItemDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.StockQuantity,
                    Image = od.Product.Image
                }).ToList()))
                .ReverseMap();

            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Order.OrderDetails.Select(od => new CartItemDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.StockQuantity,
                    Image = od.Product.Image
                }).ToList()))
                .ReverseMap();

            CreateMap<Product, ProductWithRelatedDTO>().ReverseMap();
        }
    }
}
