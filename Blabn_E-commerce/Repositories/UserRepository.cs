using Blabn_E_commerce.Models;
using Blabn_E_commerce.ViewModels;

namespace Blabn_E_commerce.Repositories
{
    public class UserRepository: IUserRepository
    {
        Blabn_Context Context;
        public UserRepository() { 
            Context = new Blabn_Context();
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
        }

        public void Delete(User user)
        {
            Context.Users.Remove(user);
        }

        public User Get(int id)
        {
            return Context.Users.FirstOrDefault(c => c.UserId == id);
        }

        public List<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(User user)
        {
            Context.Update(user);
        }
        public User GetByEmail(String Email)
        {
            return Context.Users.FirstOrDefault(c=>c.Email == Email);
        }

        public void Add(SignupVM signupVM)
        {
            throw new NotImplementedException();
        }

        //public void Add(SignupVM signupVM)
        //{
        //    Context.Add(signupVM);
        //}
    }
}
