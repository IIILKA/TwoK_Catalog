using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Services.Interfaces;
using TwoK_Catalog.ViewModels.Order;

namespace TwoK_Catalog.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public OrderController(IUserService userService, ICartService cartService, IOrderService orderService)
        {
            _userService = userService;
            _cartService = cartService;
            _orderService = orderService;
        }

        public ViewResult ToOrder() => View(new CreateOrderViewModel());

        [HttpPost]
        public IActionResult ToOrder(CreateOrderViewModel createOrderViewModel)
        {
            var userId = _userService.GetUserId(User);

            if (_cartService.GetCartItems(userId).Count == 0)
            {
                ModelState.AddModelError("", "Прости, но твоя корзина пуста");
            }

            if (ModelState.IsValid)
            {
                _orderService.CreateOrder(userId, createOrderViewModel);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(createOrderViewModel);
            }
        }

        [AllowAnonymous]
        public IActionResult Completed()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Authorize(Roles = "SeniorAdmin,JuniorAdmin")]
        public IActionResult List()
        {
            return View(_orderService.GetOrders().Where(o => !o.IsShipped));
        }

        [Authorize(Roles = "SeniorAdmin,JuniorAdmin")]
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _orderService.GetOrders().FirstOrDefault(_ => _.Id == orderId);

            if(order != null)
            {
                _orderService.MarkOrderAsShipped(order.Id);
            }

            return RedirectToAction(nameof(List));
        }
    }
}
