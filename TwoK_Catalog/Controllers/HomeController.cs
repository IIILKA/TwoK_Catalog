using Microsoft.AspNetCore.Mvc;

namespace TwoK_Catalog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
