using System.Linq;
using Xunit;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Tests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewItems()
        {
            //A1
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            Cart cart = new Cart();

            //A2
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartItem[] result = cart.CartItems.ToArray();

            //A3
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingItems()
        {
            //A1
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            Cart cart = new Cart();

            //A2
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);
            CartItem[] result = cart.CartItems.OrderBy(i => i.Product.Id).ToArray();

            //A3
            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void CanRemoveItems()
        {
            //A1
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };
            Product p3 = new Product { Id = 3, Name = "P3" };

            Cart cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            //A2
            cart.RemoveItem(p2);
            cart.RemoveItem(p3, 4);
            cart.RemoveItem(p1, 3);
            CartItem[] cartItems = cart.CartItems.OrderBy(i => i.Product.Id).ToArray();

            //A3
            Assert.Equal(2, cartItems.Length);
            Assert.Equal(3, cartItems.Where(i => i.Product.Id == p2.Id).FirstOrDefault().Quantity);
            Assert.Equal(1, cart.CartItems.Where(i => i.Product == p3).FirstOrDefault().Quantity);
            Assert.Equal(0, cart.CartItems.Where(i => i.Product == p1).Count());
        }

        [Fact]
        public void CalculateCartTotal()
        {
            //A1
            Product p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { Id = 2, Name = "P2", Price = 50M };
            Product p3 = new Product { Id = 3, Name = "P3", Price = 10.5M };

            Cart cart = new Cart();

            //A2
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 3);
            cart.AddItem(p1, 2);
            decimal result = cart.ComputeTotalValue();

            //A3
            Assert.Equal(431.5M, result);
        }

        [Fact]
        public void CanClearCart()
        {
            //A1
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };
            Product p3 = new Product { Id = 3, Name = "P3" };

            Cart cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            //A2
            cart.Clear();

            //A3
            Assert.Equal(0, cart.CartItems.Count());
        }
    }
}
