using TwoK_Catalog.ViewModels.Order;

namespace TwoK_Catalog.Models.ViewModels
{
    public class AccountProfileViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public string ReturnUrl { get; set; }
    }
}
