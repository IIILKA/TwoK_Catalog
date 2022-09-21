using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
