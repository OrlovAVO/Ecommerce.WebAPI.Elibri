using Elibri.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }


        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
