using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();//!!!Важная строчка, изучи!!!
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Category = "Phone",
                        Company = "Apple",
                        Name = "iPhone 13",
                        Equipment = "128GB",
                        Description = "Some description",
                        Price = 2400
                    },
                    new Product
                    {
                        Category = "Phone",
                        Company = "Xiaomi",
                        Name = "Redmi 5 Plus",
                        Equipment = "4/128GB",
                        Description = "Some description",
                        Price = 420
                    },
                    new Product
                    {
                        Category = "Phone",
                        Company = "Xiaomi",
                        Name = "Black Shark 4",
                        Equipment = "12/256GB",
                        Description = "Some description",
                        Price = 1700
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
