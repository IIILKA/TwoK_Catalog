using System.ComponentModel.DataAnnotations;

namespace TwoK_Catalog.Models.BusinessModels.Enums
{
    public enum Categories
    {
        [Display(Name = "Телефон")]
        Phone,
        [Display(Name = "Ноутбук")]
        Laptop
    }
}
