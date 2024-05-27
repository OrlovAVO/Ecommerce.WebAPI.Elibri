using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.DTOs.DTOS
{
    public class CartDTO
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}
//удаление товаров