using TwoK_Catalog.Models;
using TwoK_Catalog.Services.Interfaces;

namespace TwoK_Catalog.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<string> GetCompanyNames()
        {
            return _dbContext.Companys.Select(x => x.Name).ToList();
        }
    }
}
