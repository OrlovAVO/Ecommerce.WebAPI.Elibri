using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.DTOS
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}