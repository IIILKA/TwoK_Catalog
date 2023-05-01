using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Entities;
using TwoK_Catalog.Models;
using TwoK_Catalog.Services.Interfaces;
using TwoK_Catalog.ViewModels.CartItem;

namespace TwoK_Catalog.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;

        public CartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public List<CartItemViewModel> GetCartItems(string userId)
        {
            var cartItems = _dbContext.CartItems
                .Include(_ => _.Product)
                .ThenInclude(_ => _.Company)
                .Where(_ => _.UserId == userId)
                .ToList();

            var cartItemViewModels = new List<CartItemViewModel>();
            foreach (var item in cartItems)
            {
                cartItemViewModels.Add(new CartItemViewModel
                {
                    Id = item.Id,
                    ProductTitle = item.Product.GetTitle(),
                    ProductPrice = item.Product.Price,
                    Quantity = item.Quantity,
                });
            }

            return cartItemViewModels;
        }

        public void AddCartItem(string userId, int productId, int quantity = 1)
        {
            var cartItem = _dbContext.CartItems
                .Include(_ => _.Product)
                .FirstOrDefault(_ => _.UserId == userId && _.Product.Id == productId);

            if (cartItem == null)
            {
                var product = _dbContext.Products
                    .Include(_ => _.Company)
                    .FirstOrDefault(_ => _.Id == productId);

                if (product != null)
                {
                    _dbContext.CartItems.Add(new CartItem
                    {
                        Product = product,
                        UserId = userId,
                        Quantity = quantity
                    });
                }
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _dbContext.SaveChanges();
        }

        public void RemoveCartItem(string userId, int cartItemId)
        {
            var cartItem = _dbContext.CartItems
                .Include(_ => _.Product)
                .FirstOrDefault(_ => _.Id == cartItemId);

            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
            }

            _dbContext.SaveChanges();
        }
    }
}
