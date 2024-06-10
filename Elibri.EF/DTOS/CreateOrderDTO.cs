namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для создания заказа.
    /// </summary>
    public class CreateOrderDTO
    {
        /// <summary>
        /// Элементы корзины.
        /// </summary>
        public List<CreateCartItemDTO> CartItems { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Адрес пользователя.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Номер кредитной карты пользователя.
        /// </summary>
        public string CardNumber { get; set; }
    }
}
