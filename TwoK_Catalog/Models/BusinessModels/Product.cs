using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Отсутсвует описание продукта")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Отсутсвует цена продукта")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Недопустимое значение")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Отсутсвует конфигурация продукта")]
        public SubCategory SubCategory { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Отсутсвует файл с изображением")]
        [NotMapped]
        public IFormFile FormFile { get; set; }
        [Required(ErrorMessage = "Отсутсвует количество продукта")]
        [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
        public int Quaintity { get; set; }

        private Dictionary<string, string> descriptionValues = new Dictionary<string, string>();

        public string GetTitle() => $"{Company.Name} {Name} {Equipment}";
        
        public string[] GetDescriptionLines()
        {
            return Description.Split("\n");
        }
    }
}
