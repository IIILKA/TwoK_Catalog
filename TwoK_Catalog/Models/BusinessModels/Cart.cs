namespace TwoK_Catalog.Models.BusinessModels
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        private List<CartItem> cartItems = new List<CartItem>();
        public virtual IEnumerable<CartItem> CartItems
        {
            get { return cartItems; }
            set { cartItems = (List<CartItem>)value; }
        }

        public virtual void AddItem(Product product, int quantity = 1)
        {
            CartItem cartItem = cartItems.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (cartItem == null)
            {
                cartItems.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(Product product, int quantity = 1)
        {
            CartItem cartItem = cartItems.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (cartItem != null)
            {
                if (cartItem.Quantity > quantity)
                {
                    cartItem.Quantity -= quantity;
                }
                else
                {
                    cartItems.RemoveAll(i => i.Product.Id == product.Id);
                }
            }
        }

        public virtual void UpdateCart()
        {

        }
        public virtual decimal ComputeTotalValue() => cartItems.Sum(i => i.Product.Price * i.Quantity);

        public virtual void Clear() => cartItems.Clear();
    }

    public class CartItem
    {
        public CartItem() { }
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
