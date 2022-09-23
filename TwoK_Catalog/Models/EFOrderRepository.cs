using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.CartItems)
            .ThenInclude(ci => ci.Product)
            .ThenInclude(p => p.Company);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.CartItems.Select(l => l.Product));//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            context.AttachRange(order.CartItems);
            if (order.Id == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
