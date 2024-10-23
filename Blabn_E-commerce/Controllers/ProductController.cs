using Blabn_E_commerce.Models;
using Blabn_E_commerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO; // Ensure you have this namespace for File handling
using Microsoft.AspNetCore.Http; // Ensure you have this namespace for IFormFile

namespace Blabn_E_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUserRepository UserRepository;
        private readonly IProductRepository ProductRepository;
        public ProductController(IUserRepository userRepository, IProductRepository productRepository)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(ProductRepository.GetCategories(), "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile imageFile)
        {
            if (product == null)
            {
                return NotFound();
            }

           
            else
            {
                // Generate unique file name and save the image file
                var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                // Save the file to wwwroot/images directory
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Store the relative path to the image
                product.Image = "/images/" + fileName;
            }

            // Check model state
            if (ModelState.IsValid)
            {
                ProductRepository.Add(product);
                ProductRepository.Save();
                return RedirectToAction("ShowProducts"); // Redirect to product list after creation
            }

            // Repopulate the categories in case of validation errors
            ViewBag.Categories = new SelectList(ProductRepository.GetCategories(), "CategoryId", "Name");
            return View(product);
        }
        //view ! finished
        [HttpGet]
        
        public IActionResult ShowProducts()
        {
            // Get products as IQueryable to support Include
            var products = ProductRepository.GetAll().ToList(); // Convert to list after including
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            var product = ProductRepository.Get(id);
            if (product == null)
            {
                return NotFound(); // Return a 404 if the product is not found
            }

            ViewBag.Categories = new SelectList(ProductRepository.GetCategories(), "CategoryId", "Name");
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Update(product); // Update the product in the repository
                ProductRepository.Save();
                return RedirectToAction("ShowProducts"); // Redirect to the product list after editing
            }

            // If validation fails, repopulate the category list and return the view with the current model
            ViewBag.Categories = new SelectList(ProductRepository.GetCategories(), "CategoryId", "Name");
            return View(product);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = ProductRepository.Get(id);
            if (product == null)
            {
                return NotFound(); // Return a 404 if the product is not found
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, Product product)
        {
            //var product = ProductRepository.Get(id);
            if (product == null)
            {
                return NotFound(); // Return a 404 if the product is not found
            }

            ProductRepository.Delete(product); // Delete the product from the repository
            ProductRepository.Save();
            return RedirectToAction(nameof(ShowProducts)); // Redirect to the product list after deletion
        }

    }
}
