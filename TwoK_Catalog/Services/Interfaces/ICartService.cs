using TwoK_Catalog.ViewModels.CartItem;

namespace TwoK_Catalog.Services.Interfaces
{
    public interface ICartService
    {
        public List<CartItemViewModel> GetCartItems(string userId);

        public void AddCartItem(string userId, int productId, int quantity);

        public void RemoveCartItem(string userId, int productId);
    }
}
