using TwoK_Catalog.Entities.Core;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Entities
{
    public class OrderItem : Entity
    {
        public Order Order { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
