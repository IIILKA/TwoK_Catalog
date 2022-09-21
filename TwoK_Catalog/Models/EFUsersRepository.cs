using Microsoft.AspNetCore.Identity;
using System.Linq;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class EFUsersRepository : IUserRepository
    {
        private ApplicationIdentityDbContext context;
        private readonly UserManager<User> userManager;
        public EFUsersRepository(ApplicationIdentityDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IQueryable<User> Users => context.Users;

        public async Task SaveUserAsync(User user, string role)
        {
            if (user.Id == null)
            {
                context.Add(user);
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                User dbUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
                if(dbUser != null)
                {
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;//на всякий случай, но эти параметры всегда равны
                    await ChangeRoleAsync(dbUser, role);
                }
            }
            context.SaveChanges();
        }

        public User DeleteUser(string userId)
        {
            var dbUser = context.Users.FirstOrDefault(u => u.Id == userId);
            if(dbUser != null)
            {
                context.Remove(dbUser);
                context.SaveChanges();
            }
            return dbUser;
        }

        public async Task<string> GetRoleAsync(string userId)
        {
            User user = context.Users.FirstOrDefault(u => u.Id == userId);
            if(user != null)
            {
                if(await userManager.IsInRoleAsync(user, "User"))
                {
                    return "User";
                }
                else if(await userManager.IsInRoleAsync(user, "JuniorAdmin"))
                {
                    return "JuniorAdmin";
                }
                else if(await userManager.IsInRoleAsync(user, "SeniorAdmin"))
                {
                    return "SeniorAdmin";
                }
            }
            return "";
        }

        public async Task<string> GetRoleAsync(User user)
        {
            if (user != null)
            {
                if (await userManager.IsInRoleAsync(user, "User"))
                {
                    return "User";
                }
                else if (await userManager.IsInRoleAsync(user, "JuniorAdmin"))
                {
                    return "JuniorAdmin";
                }
                else if (await userManager.IsInRoleAsync(user, "SeniorAdmin"))
                {
                    return "SeniorAdmin";
                }
            }
            return "";
        }

        private async Task ChangeRoleAsync(User user, string role)
        {
            List<string> roles = context.Roles.Select(r => r.Name).ToList();
            foreach(var r in roles)
            {
                if(await userManager.IsInRoleAsync(user, r))
                {
                    await userManager.RemoveFromRoleAsync(user, r);
                }
            }
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
