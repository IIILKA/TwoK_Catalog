using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Controllers;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
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

        public ViewResult List(int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                    .Include(p => p.Company)
                    .Include(p => p.SubCategory)
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
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
