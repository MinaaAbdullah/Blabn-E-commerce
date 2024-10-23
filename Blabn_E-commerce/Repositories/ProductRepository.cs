using Blabn_E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Blabn_E_commerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Blabn_Context Context;

        public ProductRepository()
        {
            Context = new Blabn_Context();
        }

        public void Add(Product product)
        {
            Context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            Context.Products.Remove(product);
        }

        public Product Get(int id)
        {
            //return Context.Products.FirstOrDefault(c => c.ProductId == id);
            return Context.Products.FirstOrDefault(c => c.ProductId == id);
        }

        public List<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public IQueryable<Product> GetAll2()
        {
            return Context.Products.Include(p => p.Category); // Include category here
        } 
        public void Save()
        {
            Context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return Context.Categories.ToList();
        }

        public void Update(Product product)
        {
            Context.Products.Update(product);
        }

        // New method to check if a category exists
        public bool CategoryExists(int categoryId)
        {
            return Context.Categories.Any(c => c.CategoryId == categoryId);
        }

        public string? GetProductCategories()
        {
            throw new NotImplementedException();
        }
    }
}
