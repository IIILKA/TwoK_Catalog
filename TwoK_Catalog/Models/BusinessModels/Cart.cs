using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using TwoK_Catalog.Infrastructure;

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
        [NotMapped]
        private ICartRepository repository;

        public static Cart GetCart(IServiceProvider services)
        {
            string userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ICartRepository repository = services.GetRequiredService<ICartRepository>();
            Cart cart = repository.GetCart(userId) ?? new Cart() { UserId = userId };
            cart.repository = repository;
            return cart;
        }

        public void AddItem(Product product, int quantity = 1)
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

            repository.SaveCart(this);
        }

        public void RemoveItem(Product product, int quantity = 1)
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

            repository.SaveCart(this);
        }

        public decimal ComputeTotalValue() => cartItems.Sum(i => i.Product.Price * i.Quantity);

        public void Clear()
        {
            cartItems.Clear();
            repository.SaveCart(this);
        }
    }

    public class CartItem
    {
        public CartItem() { }
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
