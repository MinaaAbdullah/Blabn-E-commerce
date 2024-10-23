using Blabn_E_commerce.Models;

namespace Blabn_E_commerce.Repositories
{
    public interface IProductRepository
    {
        public void Add(Product product);
        public void Update(Product product);
        public void Delete(Product product);
        public List<Category> GetCategories();
        public IQueryable<Product> GetAll2();
        public List<Product> GetAll();
        public Product Get(int id);
        public void Save();
        public bool CategoryExists(int categoryId);
        public string? GetProductCategories();

    }
}
