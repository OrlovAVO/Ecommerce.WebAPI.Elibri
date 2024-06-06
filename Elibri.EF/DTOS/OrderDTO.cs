using Elibri.EF.DTOS;
using System.Collections.Generic;

namespace Elibri.EF.DTOS
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

}
