using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TwoK_Catalog.Controllers;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;

namespace TwoK_Catalog.Tests
{
    public class AdminControllerTests
    {
        private T GetViewModel<T> (IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void CanEditProduct()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" }
            }).AsQueryable<Product>());

            Mock<IUserRepository> usersMock = new Mock<IUserRepository>();

            AdminController target = new AdminController(mock.Object, usersMock.Object);

            //A2
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            //A3
            Assert.Equal(1, p1.Id);
            Assert.Equal(2, p2.Id);
            Assert.Equal(3, p3.Id);
        }

        [Fact]
        public void CannotEditNoneexistentProduct()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" }
            }).AsQueryable<Product>());

            Mock<IUserRepository> usersMock = new Mock<IUserRepository>();

            AdminController target = new AdminController(mock.Object, usersMock.Object);

            //A2
            Product p4 = GetViewModel<Product>(target.Edit(4));

            //A3
            Assert.Null(p4);
        }


        [Fact]
        public void CanSaveValidChanges()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            Mock<IUserRepository> usersMock = new Mock<IUserRepository>();

            //Mock<IWebHostEnvironment> appEnvironment = new Mock<IWebHostEnvironment>();

            AdminController target = new AdminController(mock.Object, usersMock.Object) { TempData = tempData.Object };
            target.ModelState.AddModelError("FormFile", "FormFileError");
            target.ModelState.AddModelError("ImagePath", "ImagePathError");

            Product product = new Product() { Name = "P1" };

            //A2
            IActionResult result = target.Edit(product);

            //A3
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CRUDproducts", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void CannotSaveInvalidChanges()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            Mock<IUserRepository> usersMock = new Mock<IUserRepository>();

            //Mock<IWebHostEnvironment> appEnvironment = new Mock<IWebHostEnvironment>();

            AdminController target = new AdminController(mock.Object, usersMock.Object);
            target.ModelState.AddModelError("FormFile", "FormFileError");
            target.ModelState.AddModelError("ImagePath", "ImagePathError");

            Product product = new Product() { Name = "P1" };

            target.ModelState.AddModelError("error", "error");

            //A2
            IActionResult result = target.Edit(product);

            //A3
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CanDeleteValidProduct()
        {
            //A1
            Product p2 = new Product() { Id = 2, Name = "P2" };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product() { Id = 1, Name = "P1" },
                p2,
                new Product() { Id = 3, Name = "P3" }
            }).AsQueryable<Product>());

            Mock<IUserRepository> usersMock = new Mock<IUserRepository>();

            AdminController target = new AdminController(mock.Object, usersMock.Object);

            //A2
            target.Delete(p2.Id);

            //A3
            mock.Verify(m => m.DeleteProduct(p2.Id));
        }
    }
}
