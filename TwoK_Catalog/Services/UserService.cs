using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TwoK_Catalog.Models.BusinessModels;
using TwoK_Catalog.Services.Interfaces;

namespace TwoK_Catalog.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            string userId = _userManager.GetUserId(user);

            return userId;
        }
    }
}
