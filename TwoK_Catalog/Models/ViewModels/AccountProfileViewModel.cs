using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models.ViewModels
{
    public class AccountProfileViewModel
    {
        public List<Order> Orders { get; set; }
        public string ReturnUrl { get; set; }
    }
}
