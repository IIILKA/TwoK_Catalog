using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class Initializer
    {
        public static async Task CreateDefaultCopanys(ApplicationDbContext context)
        {
            Company[] companys = new Company[]
            {
                new Company(0, "Apple", @"/img/producers/apple.jpg"),
                new Company(0, "Google", @"/img/producers/google.jpg"),
                new Company(0, "Honor", @"/img/producers/honor.jpg"),
                new Company(0, "Huawey", @"/img/producers/huawei.jpg"),
                new Company(0, "Nokia", @"/img/producers/nokia.jpg"),
                new Company(0, "OnePlus", @"/img/producers/oneplus.jpg"),
                new Company(0, "Oppo", @"/img/producers/oppo.png"),
                new Company(0, "Realmi", @"/img/producers/realmi.svg"),
                new Company(0, "Samsung", @"/img/producers/samsung.jpg"),
                new Company(0, "Sony", @"/img/producers/sony.jpg"),
                new Company(0, "Vivo", @"/img/producers/vivo.jpg"),
                new Company(0, "Xiaomi", @"/img/producers/xiaomi.png")
            };

            foreach(var company in companys)
            {
                Company dbCompany = context.Companys.FirstOrDefault(c => c.Name == company.Name);
                if(dbCompany == null)
                {
                    context.Companys.Add(company);
                }
                else
                {
                    dbCompany.ImgPath = company.ImgPath;
                }
            }

            context.SaveChanges();
        }

        public static async Task CreateDefaultCategories(ApplicationDbContext context)
        {
            context.AttachRange(context.Companys);
            SubCategory[] subCategories = new SubCategory[]
            {
                new SubCategory(0, "Видеокарты", "GraphicsCards", new List<Company>()),
                new SubCategory(0, "Жесткие диски", "HardDisks", new List<Company>()),
                new SubCategory(0, "Твердотельные накопители (SSD)", "SSD", new List<Company>()),
                new SubCategory(0, "Мониторы", "Monitors", new List<Company>()),
                new SubCategory(0, "Ноутбуки", "Notebooks", new List<Company>()),
                new SubCategory(0, "Мобильные телефоны", "Mobile", context.Companys.ToList()),
                new SubCategory(0, "Планшеты", "PcTablets", new List<Company>()),
                new SubCategory(0, "Чехлы для телефонов", "MobileCases", new List<Company>()),
                new SubCategory(0, "Портативные зарядные устройства", "PortableChargers", new List<Company>())
            };

            Category[] categories = new Category[]
            {
                new Category(0, "Компьютерная техника", "Komp", new List<SubCategory>() 
                { 
                    subCategories[0], 
                    subCategories[1],
                    subCategories[2],
                    subCategories[3],
                    subCategories[4]
                }),
                new Category(0, "Телефония и связь", "Phone", new List<SubCategory>()
                {
                    subCategories[5],
                    subCategories[6],
                    subCategories[7],
                    subCategories[8]
                })
            };
            
            if(context.SubCategories.Count() == 0)
            {
                context.SubCategories.AddRange(subCategories);
                context.Categories.AddRange(categories);
            }

            context.SaveChanges();
        }
    }
}
