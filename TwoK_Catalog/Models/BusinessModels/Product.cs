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

        public Dictionary<string, string> DescriptionValues
        {
            get
            {
                if (descriptionValues.Count < 1)
                    UpdateDescriptionValues();
                return descriptionValues;
            }
        }

        private void UpdateDescriptionValues()
        {
            descriptionValues.Clear();
            string[] parts = Description.Split("\n");
            descriptionValues.Add("Экран", parts[0].Replace("Экран: ", ""));
            descriptionValues.Add("Система", parts[1].Replace("Система: ", ""));
            descriptionValues.Add("Память", parts[2].Replace("Память: ", ""));
            descriptionValues.Add("Камера", parts[3].Replace("Камера: ", ""));
            descriptionValues.Add("Связь", parts[4].Replace("Связь: ", ""));
            descriptionValues.Add("Конструкция", parts[5].Replace("Конструкция: ", ""));
        }

        public string GetTitle() => $"{Company.Name} {Name} {Equipment}";

        public string GetDescriptionValue(string key) => DescriptionValues[key];
    }
}
