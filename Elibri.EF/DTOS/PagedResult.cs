namespace Elibri.EF.DTOS
{
    /// <summary>
    /// DTO для результата постраничного запроса.
    /// </summary>
    /// <typeparam name="T">Тип элементов в результирующем списке.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Элементы на странице.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Общее количество элементов.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
