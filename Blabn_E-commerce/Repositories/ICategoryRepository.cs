using Blabn_E_commerce.Models;
using System.Collections.Generic;

namespace Blabn_E_commerce.Repositories
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(Category category);
        public Category Get(int id);
        public List<Category> GetAll();
        public void Save();
    }

}
