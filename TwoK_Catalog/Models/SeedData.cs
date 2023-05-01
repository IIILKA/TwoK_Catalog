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
                context.AttachRange(context.Companys);
                context.AttachRange(context.SubCategories);
                context.Products.AddRange(
                    new Product
                    {
                        SubCategory = context.SubCategories.FirstOrDefault(sc => sc.Name_UK == "Mobile"),
                        Company = context.Companys.FirstOrDefault(c => c.Name == "Apple"),
                        Name = "iPhone 13",
                        Equipment = "128GB",
                        Description = "Экран: 6.1'', 1170x2532 px, AMOLED\r\n" +
                                    "Система: iOS 15, Apple A15 Bionic, 6-ядерный\r\n" +
                                    "Память: ОЗУ: 6144 Мб, флэш: 128 Гб\r\n" +
                                    "Камера: двойная, основная: 12 Мп, фронтальная: 12 Мп, видео: 3840x2160\r\n" +
                                    "Связь: SIM-карт: 1, LTE, Bluetooth 5.0, NFC, выход на наушники: lightning\r\n" +
                                    "Конструкция: корпус: металл и стекло, сканер радужки, сканер лица, водозащита, Li-Ion",
                        Price = 2400,
                        ImagePath = "/img/products/mobiles/apple/i6aedda15b.png",
                        Quaintity = 100
                    },
                    new Product
                    {
                        SubCategory = context.SubCategories.FirstOrDefault(sc => sc.Name_UK == "Mobile"),
                        Company = context.Companys.FirstOrDefault(c => c.Name == "Xiaomi"),
                        Name = "Redmi 5 Plus",
                        Equipment = "4/128GB",
                        Description = "Экран: 6.1'', 1170x2532 px, AMOLED\r\n" +
                                    "Система: iOS 15, Apple A15 Bionic, 6-ядерный\r\n" +
                                    "Память: ОЗУ: 6144 Мб, флэш: 128 Гб\r\n" +
                                    "Камера: двойная, основная: 12 Мп, фронтальная: 12 Мп, видео: 3840x2160\r\n" +
                                    "Связь: SIM-карт: 1, LTE, Bluetooth 5.0, NFC, выход на наушники: lightning\r\n" +
                                    "Конструкция: корпус: металл и стекло, сканер радужки, сканер лица, водозащита, Li-Ion",
                        Price = 420,
                        ImagePath = "/img/products/mobiles/xiaomi/Xiaomi Redmi 5 Plus.jpg",
                        Quaintity = 100
                    },
                    new Product
                    {
                        SubCategory = context.SubCategories.FirstOrDefault(sc => sc.Name_UK == "Mobile"),
                        Company = context.Companys.FirstOrDefault(c => c.Name == "Xiaomi"),
                        Name = "Black Shark 4",
                        Equipment = "12/256GB",
                        Description = "Экран: 6.1'', 1170x2532 px, AMOLED\r\n" +
                                    "Система: iOS 15, Apple A15 Bionic, 6-ядерный\r\n" +
                                    "Память: ОЗУ: 6144 Мб, флэш: 128 Гб\r\n" +
                                    "Камера: двойная, основная: 12 Мп, фронтальная: 12 Мп, видео: 3840x2160\r\n" +
                                    "Связь: SIM-карт: 1, LTE, Bluetooth 5.0, NFC, выход на наушники: lightning\r\n" +
                                    "Конструкция: корпус: металл и стекло, сканер радужки, сканер лица, водозащита, Li-Ion",
                        Price = 1700,
                        ImagePath = "/img/products/mobiles/xiaomi/Xiaomi Black Shark 4.jpg",
                        Quaintity = 100
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
