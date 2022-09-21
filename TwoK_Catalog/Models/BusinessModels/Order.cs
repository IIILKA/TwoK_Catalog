using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TwoK_Catalog.Models.BusinessModels
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }
        [BindNever]
        public bool IsShipped { get; set; }
        [BindNever]
        [Required(AllowEmptyStrings = true)]
        public ICollection<CartItem> CartItems { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите постовый индекс")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Введите адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Введите название города")]
        public string City { get; set; }
        [Required(ErrorMessage = "Введите название страны")]
        public string Country { get; set; }
        public string Color { get; set; }

        [BindNever]
        public string UserId { get; set; }
    }
}
