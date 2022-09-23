using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Diagnostics.CodeAnalysis;

namespace TwoK_Catalog.Models.BusinessModels
{
    public class Company
    {
        public int Id { get; set; }
        [ValidateNever]
        public string Name { get; set; }
        [ValidateNever]
        public string ImgPath { get; set; }

        public Company() { }
        public Company(int id, string name, string imgPath)
        {
            Id = id;
            Name = name;
            ImgPath = imgPath;
        }
    }
}
