using Elibri.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.Models
{
    public class Cart
    {
        [Key]
        [Required]
        public int CartId { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
