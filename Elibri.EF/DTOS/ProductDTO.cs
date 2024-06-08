using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.DTOS
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Warranty { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public int DeliveryDays { get; set; }

    }
}
