using Microsoft.AspNetCore.Identity;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Идентификатор корзины пользователя.
        /// </summary>
        public int? CartId { get; set; }

        /// <summary>
        /// Корзина пользователя.
        /// </summary>
        public Cart Cart { get; set; }

        /// <summary>
        /// Список заказов, оформленных пользователем.
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}
