using TwoK_Catalog.Entities.Core;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Entities
{
    public class CartItem : Entity
    {
        public string UserId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
