using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNumber { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }


        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
