using System.Linq;
using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class EFCartRepository : ICartRepository
    {
        private ApplicationDbContext context;
        public EFCartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Cart? GetCart(string userId)
        {
            Cart? cart = context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);
            return cart;
        }

        public void SaveCart(Cart cart)
        {
            if(cart != null)
            {
                Cart? dbCart = context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefault(c => c.Id == cart.Id);
                if(dbCart != null)
                {
                    dbCart.CartItems = cart.CartItems;
                    dbCart.UserId = cart.UserId;//на всякий случай, хотя эти параметры всегда равны
                }
                else
                {
                    context.Carts.Add(cart);
                }
                context.SaveChanges();
            }
        }
    }
}
