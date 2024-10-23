using Microsoft.AspNetCore.Mvc;
using Blabn_E_commerce.Models;
using Blabn_E_commerce.Repositories;
using Blabn_E_commerce.ViewModels;
using System.Security.Claims;

namespace Blabn_E_commerce.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository UserRepository;
        private readonly IProductRepository ProductRepository;
        public UserController(IUserRepository userRepository, IProductRepository productRepository)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID
            ViewBag.UserId = userId; // Pass it to the view
            var products = ProductRepository.GetAll(); // Assuming you have a method to get all products
            return View(products);
        }
        public IActionResult Signup() { return View(); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Email.EndsWith("@admin.com"))
                {
                    user.IsAdmin = true;
                    UserRepository.Add(user);
                    UserRepository.Save();
                    
                    return RedirectToAction(nameof(Showall));
                }
                else
                {
                    user.IsAdmin = false;
                    UserRepository.Add(user);
                    UserRepository.Save();
                    return RedirectToAction(nameof(ShowProducts));
                }
            }
            return RedirectToAction(nameof(Showall));
        }
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost] // Explicitly specify this is a POST method
        [ValidateAntiForgeryToken]
        public IActionResult Signin(SigninVM model)
        {
            if (ModelState.IsValid)
            {
                var user = UserRepository.GetByEmail(model.Email);

                if (user == null)
                {
                    return NotFound();
                }
                if (user.Password == model.Password) // Replace with a secure password check
                {
                    if (user.IsAdmin)
                    {
                        
                        return RedirectToAction(nameof( Showall));
                         
                    }
                    else
                    {
                        return RedirectToAction(nameof(ProductController.ShowProducts));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
                
            }
            return View(model);
        }

        public IActionResult Showall()
        {
            List<User> users = UserRepository.GetAll();
            return View(users);
        }

        //public IActionResult ShowProducts()
        //{
        //    List<Product> products = ProductRepository.GetAll();
        //    return View(products);
        //}

    }
}
