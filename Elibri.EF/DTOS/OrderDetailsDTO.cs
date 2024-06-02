using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.DTOS
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public decimal TotalAmount { get; set; }



    }
}
