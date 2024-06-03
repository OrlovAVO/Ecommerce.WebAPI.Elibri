using Elibri.EF.DTOS;
using System.Collections.Generic;

namespace Elibri.EF.DTOS
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }
        public string UserId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNumber { get; set; }
    }

}
