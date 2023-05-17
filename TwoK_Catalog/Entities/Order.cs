using TwoK_Catalog.Entities.Core;

namespace TwoK_Catalog.Entities
{
    public class Order : Entity
    {
        public bool IsShipped { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string PersonName { get; set; }

        public string PostCode { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string UserId { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
