using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class FakeProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Category = "Phone", Company = "Apple", Name = "iPhone 13", Price = 950 },
            new Product { Category = "Phone", Company = "Xiaomi", Name = "Redmi 5 Plus", Price = 200 },
            new Product { Category = "Laptop", Company = "Asus", Name = "TUF Gaming FX505dv", Price = 1000 }
        }.AsQueryable();
    }
}
