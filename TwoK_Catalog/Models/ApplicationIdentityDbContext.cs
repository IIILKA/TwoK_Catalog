using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> contextOptions) 
            : base(contextOptions)
        {
            Database.EnsureCreated();
        }

        public static async Task CreateDefaultRoles(IApplicationBuilder app, IConfiguration configuration)
        {
            using var scope = app.ApplicationServices.CreateScope();//!!!Важная строчка, изучи!!!
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IEnumerable<string> roles = configuration.GetSection("Data:RolesList").GetChildren().Select(c => c.Value);

            foreach(var role in roles)
            {
                if(await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateSeniorAdminAccount(IApplicationBuilder app, IConfiguration configuration)
        {
            using var scope = app.ApplicationServices.CreateScope();//!!!Важная строчка, изучи!!!
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:SeniorAdminUser:Name"];
            string email = configuration["Data:SeniorAdminUser:Email"];
            string password = configuration["Data:SeniorAdminUser:Password"];
            string role = configuration["Data:SeniorAdminUser:Role"];

            if(await userManager.FindByNameAsync(username) == null)
            {
                if(await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                User user = new User()
                {
                    UserName = username,
                    Email = email
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
