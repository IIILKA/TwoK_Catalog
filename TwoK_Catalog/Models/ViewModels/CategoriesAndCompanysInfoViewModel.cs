using Microsoft.EntityFrameworkCore;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models.ViewModels
{
    public class CategoriesAndCompanysInfoViewModel
    {
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Company> Companys { get; set; }
        
        static public CategoriesAndCompanysInfoViewModel GetCategoriesAndCompanysInfoViewModel(IServiceProvider service)
        {
            ApplicationDbContext context = service.GetRequiredService<ApplicationDbContext>();

            var result = new CategoriesAndCompanysInfoViewModel();
            result.Categories = context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.Companys)
                .ToList();

            result.SubCategories = context.SubCategories
                .Include(sc => sc.Companys)
                .ToList();

            result.Companys = context.Companys.ToList();

            return result;
        }
    }
}
