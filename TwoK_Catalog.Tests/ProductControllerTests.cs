using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TwoK_Catalog.Models;
using TwoK_Catalog.Controllers;
using Microsoft.AspNetCore.Mvc;
using TwoK_Catalog.Models.ViewModels;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
                new Product { Id = 4, Name = "P4" },
                new Product { Id = 5, Name = "P5" },
                new Product { Id = 6, Name = "P6" },
                new Product { Id = 7, Name = "P7" },
                new Product { Id = 8, Name = "P8" }
            }).AsQueryable<Product>());

            var productController = new ProductController(mock.Object) { PageSize = 3 };

            //Действие
            var result = productController.List(3).ViewData.Model as ProductsListViewModel;

            //Утверждение
            var productArr = result?.Products.ToArray();
            Assert.NotNull(productArr);
            Assert.True(productArr.Length == 2);
            Assert.Equal("P7", productArr[0].Name);
            Assert.Equal("P8", productArr[1].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
                new Product { Id = 4, Name = "P4" },
                new Product { Id = 5, Name = "P5" },
                new Product { Id = 6, Name = "P6" },
                new Product { Id = 7, Name = "P7" },
                new Product { Id = 8, Name = "P8" }
            }).AsQueryable<Product>());

            var productController = new ProductController(mock.Object) { PageSize = 3 };

            //Действие
            var result = productController.List(3).ViewData.Model as ProductsListViewModel;

            //Утверждение
            var pageInfo = result?.PageInfo;
            Assert.NotNull(pageInfo);
            Assert.Equal(3, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(8, pageInfo.TotalItems);
            Assert.Equal(3, pageInfo.TotalPages);
        }
    }
}
