using AutoMapper;
using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Repositories.CartRepo;
using Elibri.Services.GenericServices;


namespace Elibri.Services.CartServices
{
    public class CartService : GenericService<Cart, CartDTO>, ICartService
    {
        public CartService(ICartRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}
