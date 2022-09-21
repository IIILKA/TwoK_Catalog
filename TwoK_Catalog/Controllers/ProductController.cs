using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Controllers;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;

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
            => View(new ProductsListViewModel {
                Products = repository.Products
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PageInfo = new PageInfo {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });
    }
}
