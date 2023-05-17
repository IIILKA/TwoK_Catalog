namespace TwoK_Catalog.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public bool IsShipped { get; set; }

        public string PersonName { get; set; }

        public string PostCode { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
