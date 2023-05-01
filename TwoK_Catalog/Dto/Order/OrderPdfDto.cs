namespace TwoK_Catalog.Dto.Order
{
    public class OrderPdfDto
    {
        public int Id { get; set; }

        public string PersonName { get; set; }

        public string PostCode { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<OrderItemPdfDto> OrderItems { get; set; }
    }
}
