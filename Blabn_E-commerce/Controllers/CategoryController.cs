using Blabn_E_commerce.Models;
using Blabn_E_commerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blabn_E_commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = CategoryRepository.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Populate categories for the dropdown
            ViewBag.Categories = CategoryRepository.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Add(category);
                CategoryRepository.Save();
                return RedirectToAction("Index"); // Redirect to category list after creation
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = CategoryRepository.Get(id);
            if (category == null)
            {
                return NotFound(); // Return a 404 if the category is not found
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = CategoryRepository.Get(category.CategoryId); 
                if (existingCategory == null)
                {
                    return NotFound(); 
                }
                
                existingCategory.Name = category.Name;

                CategoryRepository.Update(existingCategory);
                CategoryRepository.Save();
                return RedirectToAction("Index"); 
            }
            return View(category);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = CategoryRepository.Get(id);
            if (category == null)
            {
                return NotFound(); // Return a 404 if the category is not found
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = CategoryRepository.Get(id);
            if (category == null)
            {
                return NotFound(); // Return a 404 if the category is not found
            }
            CategoryRepository.Delete(category);
            CategoryRepository.Save();
            return RedirectToAction("Index"); // Redirect to category list after deletion
        }
    }
}
