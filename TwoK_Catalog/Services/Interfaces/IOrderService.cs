using TwoK_Catalog.ViewModels.Order;

namespace TwoK_Catalog.Services.Interfaces
{
    public interface IOrderService
    {
        public List<OrderViewModel> GetOrders();

        public List<OrderViewModel> GetOrdersByUser(string userId);

        public void CreateOrder(string userId, CreateOrderViewModel createOrderViewModel);

        public void MarkOrderAsShipped(int orderId);
    }
}
