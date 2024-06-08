using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string Type { get; set; }

        public string Color { get; set; }

        public string Warranty { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public string Image { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }

        public string Image4 { get; set; }


        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<Review> Reviews { get; set; }

        [Required]
        public int DeliveryDays { get; set; }

    }
}
