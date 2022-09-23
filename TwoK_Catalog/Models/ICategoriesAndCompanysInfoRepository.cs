using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public interface ICategoriesAndCompanysInfoRepository
    {
        IQueryable<Company> Companys { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<SubCategory> SubCategories { get; }
    }
}
