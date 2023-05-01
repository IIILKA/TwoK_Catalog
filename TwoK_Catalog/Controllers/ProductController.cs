using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TwoK_Catalog.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 2;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        //TODO: refactor this method(session)
        public ViewResult List(int? minPrice, int? maxPrice, int productPage = 1)
        {
            if (HttpContext.Session.GetInt32("minPrice") == null)
            {
                HttpContext.Session.SetInt32("minPrice", minPrice ?? 0);
            }
            else if (HttpContext.Session.GetInt32("minPrice") != null && minPrice != null)
            {
                HttpContext.Session.SetInt32("minPrice", minPrice.Value);
            }

            if (HttpContext.Session.GetInt32("maxPrice") == null)
            {
                HttpContext.Session.SetInt32("maxPrice", maxPrice ?? int.MaxValue);
            }
            else if (HttpContext.Session.GetInt32("maxPrice") != null && maxPrice != null)
            {
                HttpContext.Session.SetInt32("maxPrice", maxPrice.Value);
            }

            var products = repository.Products
                .Include(p => p.Company)
                .Include(p => p.SubCategory)
                .Where(p => p.Price >= decimal.Parse(HttpContext.Session.GetInt32("minPrice").ToString()!) &&
                            p.Price <= decimal.Parse(HttpContext.Session.GetInt32("maxPrice").ToString()!))
                .ToList();

            return View(new ProductsListViewModel
            {
                Products = products
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count()
                }
            });
        }

        public IActionResult ProductPage(int productId)
        {
            var product = repository.Products
                .Include(p => p.Company)
                .Include(p => p.SubCategory)
                .FirstOrDefault(p => p.Id == productId);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction(nameof(List));
            }
        }
    }
}
