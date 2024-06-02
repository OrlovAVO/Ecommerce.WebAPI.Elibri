using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Elibri.EF.Models
{
    public class User : IdentityUser
    {
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
