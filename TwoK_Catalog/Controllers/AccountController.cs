using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using TwoK_Catalog.Models.BusinessModels;
using TwoK_Catalog.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace TwoK_Catalog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly HttpContext context;
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor context, IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context.HttpContext;
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SessionRegisterViewModel model)
        {
            ModelState["Session"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            ModelState["Session"].Errors.Clear();
            model.Session = context.Session;
            model.LastActivity = DateTime.Now;

            if (ModelState.IsValid)
            {
                User user = new User() { UserName = model.Name, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    model.RemoveSucsessRegisterViewModel();
                    await userManager.AddToRoleAsync(user, "User");
                    //Установка куки
                    await signInManager.SignInAsync(user, false);
                    return Redirect(model.ReturnUrl);
                }
            }
            model.IsFailed = true;
            model.ErrorMessages = ModelState.Values.Where(e => e.Errors.Count > 0)
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            model.SaveFailedRegisterViewModel();
            return Redirect(model.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(string returnUrl)
        {
            SessionLogInViewModel sessionLogInViewModel = new SessionLogInViewModel();
            sessionLogInViewModel.Session = context.Session;
            sessionLogInViewModel.LastActivity = DateTime.Now;
            sessionLogInViewModel.ReturnUrl = returnUrl;
            sessionLogInViewModel.IsFailed = true;//Если равно true попап с авторизацией будет сразу открыт
            if(context.Session.GetString("AddToCartErrorMessage") != null)
            {
                ModelState.AddModelError("", context.Session.GetString("AddToCartErrorMessage"));
            }
            sessionLogInViewModel.ErrorMessages = ModelState.Values.Where(e => e.Errors.Count > 0)
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            sessionLogInViewModel.SaveFailedLogInViewModel();
            return Redirect(sessionLogInViewModel.ReturnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(SessionLogInViewModel model)
        {
            ModelState["Session"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            ModelState["Session"].Errors.Clear();
            model.Session = context.Session;
            model.LastActivity = DateTime.Now;

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        model.RemoveSucsessLogInViewModel();
                        // проверяем, принадлежит ли URL приложению
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            model.IsFailed = true;
            model.ErrorMessages = ModelState.Values.Where(e => e.Errors.Count > 0)
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            model.SaveFailedLogInViewModel();
            return Redirect(model.ReturnUrl);
        }

        public async Task<IActionResult> LogOut(string returnUrl)
        {
            // удаляем аутентификационные куки
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public async Task<ViewResult> Profile(string returnUrl)
        {
            if (User.IsInRole("SeniorAdmin"))
            {
                return View("SeniorAdminProfile", returnUrl);
            }
            else if (User.IsInRole("JuniorAdmin"))
            {
                return View("JuniorAdminProfile", returnUrl);
            }
            else
            {
                var userOrders = from order in orderRepository.Orders
                                 where order.UserId == userManager.GetUserId(User)
                                 select order;
                return View(new AccountProfileViewModel { Orders = userOrders.ToList(), ReturnUrl = returnUrl });
            }
        }
    }
}
