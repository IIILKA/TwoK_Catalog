using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using System.Diagnostics.CodeAnalysis;

namespace TwoK_Catalog.Models.BusinessModels
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Отсутсвует имя продукта")]
        public string Name { get; set; }
        public Company Company { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Equipment { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Отсутсвует цена продукта")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public decimal Price { get; set; }
        public SubCategory SubCategory { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string ImagePath { get; set; }
        [Required(AllowEmptyStrings = true)]
        [NotMapped]
        public IFormFile FormFile { get; set; }
        [Required(ErrorMessage = "Отсутсвует количество продукта")]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимое значение")]
        public int Quaintity { get; set; }

        private Dictionary<string, string> descriptionValues = new Dictionary<string, string>();

        public string GetTitle() => $"{Company.Name} {Name} {Equipment}";
        
        public string[] GetDescriptionLines()
        {
            return Description.Split("\n");
        }
    }
}
