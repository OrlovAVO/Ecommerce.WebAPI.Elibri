using System.ComponentModel.DataAnnotations;

namespace Elibri.EF.Models
{
    /// <summary>
    /// Модель заказа.
    /// </summary>
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, оформившего заказ.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Дата оформления заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Дата доставки заказа.
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Список деталей заказа.
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Имя заказчика.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия заказчика.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Адрес доставки заказа.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Номер телефона заказчика.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Номер кредитной карты заказчика.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Пользователь, оформивший заказ.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Общая стоимость заказа.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Order"/>.
        /// </summary>
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
