namespace TwoK_Catalog.ViewModels.CartItem
{
    public class CartViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }

        public decimal TotalPrice { get; set; }

        public string ReturnUrl { get; set; }
    }
}
