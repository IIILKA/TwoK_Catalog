using System.Security.Claims;

namespace TwoK_Catalog.Services.Interfaces
{
    public interface IUserService
    {
        public string GetUserId(ClaimsPrincipal user);
    }
}
