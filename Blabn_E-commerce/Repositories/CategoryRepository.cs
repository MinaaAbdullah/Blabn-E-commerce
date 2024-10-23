using Blabn_E_commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blabn_E_commerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Blabn_Context Context;

        public CategoryRepository()
        {
            Context = new Blabn_Context();
        }

        public void Add(Category category)
        {
            Context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            Context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            Context.Categories.Remove(category);
        }

        public Category Get(int id)
        {
            return Context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
