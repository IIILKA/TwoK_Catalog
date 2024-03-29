﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TwoK_Catalog.Models.BusinessModels;

namespace TwoK_Catalog.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        IWebHostEnvironment appEnvironment;
        public EFProductRepository(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            this.context = context;
            this.appEnvironment = appEnvironment;
        }

        public IQueryable<Product> Products => context.Products
            .Include(p => p.Company)
            .Include(p => p.SubCategory);

        public void SaveProduct(Product product)
        {
            if(product.Id == 0)
            {
                if (product.FormFile != null)
                {
                    string path = $"/img/products/{product.SubCategory.Name_UK.ToLower()}s/{product.Company.Name.ToLower()}/";
                    if (!Directory.Exists(appEnvironment.WebRootPath + path))
                    {
                        Directory.CreateDirectory(appEnvironment.WebRootPath + path);
                    }
                    path += product.FormFile.FileName;
                    product.ImagePath = path;
                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        product.FormFile.CopyTo(fileStream);
                    }
                }
                context.Products.Add(product);
            }
            else
            {
                Product dbProduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
                if(dbProduct != null)
                {
                    dbProduct.Name = product.Name;
                    dbProduct.Company = product.Company;
                    dbProduct.Equipment = product.Equipment;
                    dbProduct.Description = product.Description;
                    dbProduct.Price = product.Price;
                    dbProduct.SubCategory = product.SubCategory;
                    dbProduct.Quaintity = product.Quaintity;
                    if(product.FormFile != null)
                    {
                        string path = $"/img/products/{dbProduct.SubCategory.Name_UK.ToLower()}s/{dbProduct.Company.Name.ToLower()}/"; //+ product.FormFile.FileName;
                        if (!Directory.Exists(appEnvironment.WebRootPath + path))
                        {
                            Directory.CreateDirectory(appEnvironment.WebRootPath + path);
                        }
                        path += product.FormFile.FileName;
                        dbProduct.ImagePath = path;
                        using(var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            product.FormFile.CopyTo(fileStream);
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbProduct = context.Products
                .Include(p => p.Company)
                .FirstOrDefault(p => p.Id == productId);
            if( dbProduct != null)
            {
                context.Products.Remove(dbProduct);
                context.SaveChanges();
            }
            return dbProduct;
        }
    }
}
