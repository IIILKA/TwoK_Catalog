using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TwoK_Catalog.Models.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        public List<string>? ErrorMessages { get; set; }

        public bool IsFailed { get; set; } = false;
    }
}
