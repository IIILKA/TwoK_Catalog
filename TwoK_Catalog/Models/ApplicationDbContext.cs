using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }


        public static async Task CreateDefaultCopanys(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();//!!!Важная строчка, изучи!!!
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await Initializer.CreateDefaultCopanys(context);
        }

        public static async Task CreateDefaultCategories(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();//!!!Важная строчка, изучи!!!
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await Initializer.CreateDefaultCategories(context);
        }
    }
}
