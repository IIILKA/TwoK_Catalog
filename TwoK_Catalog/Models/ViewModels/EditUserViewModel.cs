using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel() { }
        public EditUserViewModel(User user, string role)
        {
            UserId = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Role = role;
        }
        [AllowNull]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Отсутсвует логин пользователя")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Отсутсвует email пользователя")]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
