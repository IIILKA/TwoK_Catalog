using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task SaveUserAsync(User user, string role);
        User DeleteUser(string userId);
        Task<string> GetRoleAsync(string userId);
        Task<string> GetRoleAsync(User user);
    }
}
