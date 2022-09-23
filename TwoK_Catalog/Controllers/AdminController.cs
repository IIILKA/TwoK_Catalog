using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Models;
using TwoK_Catalog.Models.BusinessModels;
using TwoK_Catalog.Models.BusinessModels.Enums;
using TwoK_Catalog.Models.ViewModels;
using TwoK_Catalog.Views.Admin;

namespace TwoK_Catalog.Controllers
{
    [Authorize(Roles = "JuniorAdmin,SeniorAdmin")]
    public class AdminController : Controller
    {
        private IProductRepository productRepository;
        private IUserRepository userRepository;
        private ICategoriesAndCompanysInfoRepository categoriesAndCompanysInfoRepository;
        public AdminController(IProductRepository productRepository, IUserRepository userRepository, ICategoriesAndCompanysInfoRepository categoriesAndCompanysInfoRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.categoriesAndCompanysInfoRepository = categoriesAndCompanysInfoRepository;
        }

        public ViewResult CRUDproducts() => 
            View(productRepository.Products
                .Include(p => p.Company)
                .Include(p => p.SubCategory));

        public ViewResult Edit(int productId) => 
            View(productRepository.Products
                .Include(p => p.Company)
                .Include(p => p.SubCategory)
                .FirstOrDefault(p => p.Id == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ModelState["FormFile"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            ModelState["FormFile"].Errors.Clear();
            ModelState["ImagePath"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            ModelState["ImagePath"].Errors.Clear();
            if (ModelState.IsValid)
            {
                product.Company = categoriesAndCompanysInfoRepository.Companys.FirstOrDefault(c => c.Id == product.Company.Id);
                product.SubCategory = categoriesAndCompanysInfoRepository.SubCategories.FirstOrDefault(sc => sc.Id == product.SubCategory.Id);
                productRepository.SaveProduct(product);
                TempData["message"] = product.Id == 0 ? $"{product.GetTitle()} был добавлен" : $"{product.GetTitle()} был изменён";
                return RedirectToAction("CRUDproducts");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = productRepository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.GetTitle()} был удалён";
            }
            return RedirectToAction("CRUDproducts");
        }


        [Authorize(Roles = "SeniorAdmin")]
        public async Task<ViewResult> CRUDusers()
        {
            List<EditUserViewModel> users = new List<EditUserViewModel>();
            foreach(var user in userRepository.Users.ToList())
            {
                string role = await userRepository.GetRoleAsync(user);
                users.Add(new EditUserViewModel(user, role));
            }
            return View(users);
        }

        [Authorize(Roles = "SeniorAdmin")]
        public async Task<ViewResult> EditUser(string userId)
        {
            User user = userRepository.Users.FirstOrDefault(u => u.Id == userId);
            string userRole = await userRepository.GetRoleAsync(user);
            EditUserViewModel editUserViewModel = new EditUserViewModel();
            if(user != null)
            {
                editUserViewModel = new EditUserViewModel(user, userRole);
            }
            return View(editUserViewModel);
        }

        [Authorize(Roles = "SeniorAdmin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            //ModelState["Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            //ModelState["Id"].Errors.Clear();
            User user = new User()
            {
                Id = editUserViewModel.UserId,
                UserName = editUserViewModel.UserName,
                Email = editUserViewModel.Email,
            };

            if (ModelState.IsValid)
            {
                await userRepository.SaveUserAsync(user, editUserViewModel.Role);
                TempData["message"] = user.Id == null ? $"{user.UserName} был добавлен" : $"{user.UserName} был изменён";
                return RedirectToAction("CRUDusers");
            }
            else
            {
                return View(editUserViewModel);
            }
        }

        [Authorize(Roles = "SeniorAdmin")]
        public ViewResult CreateUser() => View("EditUser", new EditUserViewModel());

        [Authorize(Roles = "SeniorAdmin")]
        [HttpPost]
        public IActionResult DeleteUser(string userId)
        {
            User deletedUser = userRepository.DeleteUser(userId);
            if(deletedUser != null)
            {
                TempData["message"] = $"{deletedUser.UserName} был удалён";
            }
            return RedirectToAction("CRUDusers");
        }
    }
}
