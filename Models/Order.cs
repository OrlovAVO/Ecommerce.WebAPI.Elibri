using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }


        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }




    }
}
