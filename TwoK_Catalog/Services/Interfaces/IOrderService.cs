using TwoK_Catalog.Dto.Order;
using TwoK_Catalog.ViewModels.Order;

namespace TwoK_Catalog.Services.Interfaces
{
    public interface IOrderService
    {
        public List<OrderViewModel> GetOrders();

        public List<OrderViewModel> GetOrdersByUser(string userId);

        public OrderPdfDto? GetOrderPdf(int orderId);

        public int CreateOrder(string userId, CreateOrderViewModel createOrderViewModel);

        public void MarkOrderAsShipped(int orderId);

        public List<OrderItemStatisticViewModel> GetOrderItemsByCompany(string companyName);
    }
}
