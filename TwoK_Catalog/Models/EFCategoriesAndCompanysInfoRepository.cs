using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class EFCategoriesAndCompanysInfoRepository : ICategoriesAndCompanysInfoRepository
    {
        private ApplicationDbContext context;
        public EFCategoriesAndCompanysInfoRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Company> Companys => context.Companys;

        public IQueryable<Category> Categories => context.Categories;

        public IQueryable<SubCategory> SubCategories => context.SubCategories;
    }
}
