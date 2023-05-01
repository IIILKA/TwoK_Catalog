using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Services.Interfaces;
using TwoK_Catalog.ViewModels.CartItem;

namespace TwoK_Catalog.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly HttpContext _context;

        public CartController(IHttpContextAccessor context, IUserService userService, ICartService cartService)
        {
            _userService = userService;
            _cartService = cartService;
            _context = context.HttpContext;
        }

        public ViewResult Index(string returnUrl)
        {
            var cartItems = _cartService.GetCartItems(_userService.GetUserId(User)); 
            var totalPrice = cartItems.Sum(_ => _.ProductPrice * _.Quantity); 
            return View(new CartViewModel { CartItems = cartItems, TotalPrice = totalPrice, ReturnUrl = returnUrl});
        }

        [AllowAnonymous]
        public RedirectToActionResult AddToCart(int productId, string returnUrl, int quantity = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                _cartService.AddCartItem(_userService.GetUserId(User), productId, quantity);
                return RedirectToAction("Index", new { returnUrl });
            }
            else
            {
                _context.Session.SetString("AddToCartErrorMessage", "Авторизуйтесь чтобы пользоваться корзиной");
                return RedirectToAction("LogIn", "Account", new { returnUrl });
            }
        }

        public RedirectToActionResult RemoveFromCart(int cartItemId, string returnUrl, int quantity = 1)
        {
            _cartService.RemoveCartItem(_userService.GetUserId(User), cartItemId);
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
