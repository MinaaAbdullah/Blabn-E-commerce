using Blabn_E_commerce.Models;
using Blabn_E_commerce.ViewModels;

namespace Blabn_E_commerce.Repositories
{
    public interface IUserRepository
    {
        public void Add(User user);

        public void Update(User user);  
        
        public void Delete(User user);
        
        public User Get(int id);
        public User GetByEmail(String Email);
        public List<User> GetAll();
        
        public void Save();
        void Add(SignupVM signupVM);
    }
}
