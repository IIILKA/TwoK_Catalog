namespace TwoK_Catalog.ViewModels.Order
{
    public class OrderItemStatisticViewModel
    {
        public int Id { get; set; }

        //TODO: this property should remove
        public string Company { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
