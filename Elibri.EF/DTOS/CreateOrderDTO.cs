using System.Collections.Generic;

namespace Elibri.EF.DTOS
{
    public class CreateOrderDTO
    {
        public List<CartItemDTO> CartItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNumber { get; set; }
    }

}
