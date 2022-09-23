using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Infrastructure;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.BusinessModels;
using TwoK_Catalog.Models.ViewModels;

namespace TwoK_Catalog.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IProductRepository productRepository;
        private Cart cart;
        private readonly HttpContext context;
        public CartController(IProductRepository productRepository, Cart cart, IHttpContextAccessor context)
        {
            this.productRepository = productRepository;
            this.cart = cart;
            this.context = context.HttpContext;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });    
        }

        [AllowAnonymous]
        public RedirectToActionResult AddToCart(int productId, string returnUrl, int quantity = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                Product product = productRepository.Products
                    .Include(p => p.Company)
                    .FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    cart.AddItem(product);
                }
                return RedirectToAction("Index", new { returnUrl });
            }
            else
            {
                context.Session.SetString("AddToCartErrorMessage", "Авторизуйтесь чтобы пользоваться корзиной");
                return RedirectToAction("LogIn", "Account", new { returnUrl });
            }
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl, int quantity = 1)
        {
            Product product = productRepository.Products
                .Include(p => p.Company)
                .FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
