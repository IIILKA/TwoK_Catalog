using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Diagnostics.CodeAnalysis;

namespace TwoK_Catalog.Models.BusinessModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name_RU { get; set; }
        public string Name_UK { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public Category() { }
        public Category(int id, string name_RU, string name_UK, List<SubCategory> subCategories)
        {
            Id = id;
            Name_RU = name_RU;
            Name_UK = name_UK;
            SubCategories = subCategories;
        }
    }

    public class SubCategory
    {
        public int Id { get; set; }
        [ValidateNever]
        public string Name_RU { get; set; }
        [ValidateNever]
        public string Name_UK { get; set; }
        [ValidateNever]
        public List<Company> Companys { get; set; }
        [ValidateNever]
        public Category ParentCategory { get; set; }

        public SubCategory() { }
        public SubCategory(int id, string name_RU, string name_UK, List<Company> companys)
        {
            Id = id;
            Name_RU = name_RU;
            Name_UK = name_UK;
            Companys = companys;
        }
    }
}
