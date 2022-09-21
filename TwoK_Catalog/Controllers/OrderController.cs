using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repositoryService, Cart cartService)
        {
            this.repository = repositoryService;
            cart = cartService;
        }

        public ViewResult ToOrder() => View(new Order());

        [HttpPost]
        public IActionResult ToOrder(Order order)
        {
            if(cart.CartItems.Count() == 0)
            {
                ModelState.AddModelError("", "Прости, но твоя корзина пуста");
            }
            else
            {
                order.CartItems = cart.CartItems.ToArray();
                order.UserId = cart.UserId;
                ModelState["CartItems"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                ModelState["CartItems"].Errors.Clear();
                ModelState["UserId"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                ModelState["UserId"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        [AllowAnonymous]
        public IActionResult Completed()
        {
            cart.Clear();
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
        public ViewResult List() => View(repository.Orders.Where(o => !o.IsShipped));

        [Authorize(Roles = "SeniorAdmin,JuniorAdmin")]
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.Id == orderId);
            if(order != null)
            {
                order.IsShipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
    }
}
