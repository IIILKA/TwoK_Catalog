using System.ComponentModel.DataAnnotations;

namespace TwoK_Catalog.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Введите постовый индекс")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите название города")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите название страны")]
        public string Country { get; set; }
    }
}
