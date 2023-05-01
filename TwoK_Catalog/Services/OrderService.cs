using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Entities;
using TwoK_Catalog.Models;
using TwoK_Catalog.Services.Interfaces;
using TwoK_Catalog.ViewModels.Order;

namespace TwoK_Catalog.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void CreateOrder(string userId, CreateOrderViewModel createOrderViewModel)
        {
            var cartItems = _dbContext.CartItems
                .Include(_ => _.Product)
                .ThenInclude(_ => _.Company)
                .Where(_ => _.UserId == userId)
                .ToList();

            var order = new Order
            {
                UserId = userId,
                Address = createOrderViewModel.Address,
                City = createOrderViewModel.City,
                Country = createOrderViewModel.Country,
                PersonName = createOrderViewModel.PersonName,
                PostCode = createOrderViewModel.PostCode,
                IsShipped = false
            };

            var orderItems = cartItems.Select(cartItem => new OrderItem
            {
                Order = order,
                Product = cartItem.Product,
                Quantity = cartItem.Quantity
            })
            .ToList();

            order.OrderItems = orderItems;

            _dbContext.CartItems.RemoveRange(cartItems);
            _dbContext.Orders.Add(order);

            _dbContext.SaveChanges();
        }

        public List<OrderViewModel> GetOrders()
        {
            var orders = _dbContext.Orders
                .Include(_ => _.OrderItems)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.Company)
                .ToList();

            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                IsShipped = order.IsShipped,
                OrderItems = order.OrderItems.Select(orderItem => new OrderItemViewModel
                {
                    Id = orderItem.Id,
                    ProductTitle = orderItem.Product.GetTitle(),
                    Quantity = orderItem.Quantity
                })
                .ToList(),
            })
            .ToList();

            return orderViewModels;
        }

        public List<OrderViewModel> GetOrdersByUser(string userId)
        {
            var orders = _dbContext.Orders
                .Include(_ => _.OrderItems)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.Company)
                .Where(_ => _.UserId == userId)
                .ToList();

            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                IsShipped = order.IsShipped,
                OrderItems = order.OrderItems.Select(orderItem => new OrderItemViewModel
                    {
                        Id = orderItem.Id,
                        ProductTitle = orderItem.Product.GetTitle(),
                        Quantity = orderItem.Quantity
                    })
                    .ToList(),
            })
            .ToList();

            return orderViewModels;
        }

        public void MarkOrderAsShipped(int orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(_ => _.Id == orderId);

            if (order != null)
            {
                order.IsShipped = true;
            }

            _dbContext.SaveChanges();
        }
    }
}
