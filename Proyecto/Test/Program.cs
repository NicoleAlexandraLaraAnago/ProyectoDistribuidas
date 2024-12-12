using Entities;
using System;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            addCategoryAndProduct();
        }

        static void addCategoryAndProduct()
        {
            var categories = new Categories();
            categories.CategoryName = "Test 2";
            categories.Description = "Description 2";

            var product = new Products();
            product.ProductName = "Aji";
            product.UnitPrice = 5;
            product.UnitsInStock = 500;

            categories.Products.Add(product);

            using (var repository = RepositoryFactory.CreateRepository())
            {
                repository.Create(categories);
            }
            Console.WriteLine($"Categoría: {categories.CategoryID}, Producto: { product.ProductID}");
            Console.ReadLine(); 
        }
    }
}
