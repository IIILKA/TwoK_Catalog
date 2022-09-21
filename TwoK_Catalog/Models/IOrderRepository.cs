using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
