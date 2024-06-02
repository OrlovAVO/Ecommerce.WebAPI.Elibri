using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;

namespace Elibri.Core.Features.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<User, UserDTO>().ReverseMap();


            CreateMap<Product, ProductDTO>().ReverseMap();


            CreateMap<Category, CategoryDTO>().ReverseMap();


            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.OrderDetails))
                .ReverseMap()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.CartItems));


            CreateMap<OrderDetail, OrderDetailDTO>()
                .ReverseMap();


            CreateMap<Cart, CartDTO>().ReverseMap();


            CreateMap<CartItemDTO, OrderDetailDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetailId, opt => opt.Ignore());


            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}
