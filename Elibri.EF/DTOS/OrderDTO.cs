namespace Elibri.EF.DTOS
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }

        public string OrderDateFormatted => OrderDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));
        public string DeliveryDateFormatted => DeliveryDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));
        public string FormattedOrderId => OrderId.HasValue ? $"000000-{OrderId.Value:D4}" : null;
    }
}
