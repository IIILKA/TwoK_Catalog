using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public interface ICartRepository
    {
        Cart? GetCart(string userId);
        void SaveCart(Cart cart);
    }
}
