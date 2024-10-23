using Microsoft.AspNetCore.Mvc;
using Blabn_E_commerce.Models;
using Blabn_E_commerce.ViewModels;
using Blabn_E_commerce.Repositories;
using System.Security.Claims;
using Newtonsoft.Json;


namespace Blabn_E_commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public CartController(ICartRepository cartRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        // Display the user's cart
        public IActionResult Index()
        {
           
            //var cartItems = _cartRepository.GetCartByUserId(userId); // Get the user's cart items
            //return View(cartItems);
            return View();
        }
        [HttpPost]


        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            // Check if the product exists
            var product = _productRepository.Get(productId); // Ensure this method exists in your repository
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Retrieve the current cart from the session or create a new one
            var cart = HttpContext.Session.GetString("Cart");
            List<CartItemViewModel> cartItems;

            if (string.IsNullOrEmpty(cart))
            {
                cartItems = new List<CartItemViewModel>();
            }
            else
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItemViewModel>>(cart);
            }

            // Add or update the item in the cart
            var existingItem = cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity; // Update quantity if product already exists
            }
            else
            {
                // Create a new cart item with product details
                cartItems.Add(new CartItemViewModel
                {
                    ProductId = productId,
                    ProductName = product.Name, // Assuming 'Name' is a property of the Product model
                    Price = product.Price,       // Assuming 'Price' is a property of the Product model
                    Quantity = quantity
                });
            }

            // Save updated cart back to session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            return RedirectToAction("ShowCart"); // Redirect to the cart display method
        }


        [HttpPost]
        public IActionResult EditInCart(int cartId, int quantity)
        {
            // Find the cart item by ID
            var cartItem = _cartRepository.GetCartById(cartId);
            if (cartItem != null)
            {
                // Update the quantity
                cartItem.Quantity = quantity;
                _cartRepository.UpdateCart(cartItem); // Update the cart in the repository
                _cartRepository.Save(); // Save changes to the database
            }

            return RedirectToAction(nameof(ShowCart)); // Redirect to the cart view
        }

        // Method to delete a product from the cart
        [HttpPost]
        public IActionResult DeleteFromCart(int cartId)
        {
            // Find the cart item by ID
            var cartItem = _cartRepository.GetCartById(cartId);
            if (cartItem != null)
            {
                _cartRepository.RemoveFromCart(cartId); // Remove the cart item from the repository
                _cartRepository.Save(); // Save changes to the database
            }

            return RedirectToAction(nameof(ShowCart)); // Redirect to the cart view
        }


        public IActionResult ShowCart()
        {
            // Retrieve the current cart from the session
            var cart = HttpContext.Session.GetString("Cart");
            List<CartItemViewModel> cartItems;

            if (string.IsNullOrEmpty(cart))
            {
                cartItems = new List<CartItemViewModel>();
            }
            else
            {
                // Deserialize the cart from session
                var cartList = JsonConvert.DeserializeObject<List<Cart>>(cart);

                // Map to CartItemViewModel
                cartItems = cartList.Select(item => new CartItemViewModel
                {
                    ProductId = item.ProductId,
                    // Assuming you have a method to get the product details by ID
                    ProductName = _productRepository.Get(item.ProductId)?.Name, // Ensure that the product exists
                    Price = _productRepository.Get(item.ProductId)?.Price ?? 0, // Ensure that the product exists
                    Quantity = item.Quantity
                }).ToList();
            }

            return View(cartItems); // Pass the list of CartItemViewModel to the view
        }

        //UpdateCart
        // Update the quantity of a cart item
        [HttpPost]
        public IActionResult UpdateCart(int cartId, int quantity)
        {
            // Retrieve the current cart from the session
            var cart = HttpContext.Session.GetString("Cart");
            List<Cart> cartItems;

            if (string.IsNullOrEmpty(cart))
            {
                return NotFound("Cart is empty.");
            }

            // Deserialize the cart from session
            cartItems = JsonConvert.DeserializeObject<List<Cart>>(cart);

            // Find the item to update
            var itemToUpdate = cartItems.FirstOrDefault(c => c.ProductId == cartId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity; // Update quantity
            }

            // Save updated cart back to session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            return RedirectToAction("ShowCart"); // Redirect to cart display method
        }

        //RemoveFromCart
        // Remove a product from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(int cartId)
        {
            // Retrieve the current cart from the session
            var cart = HttpContext.Session.GetString("Cart");
            List<Cart> cartItems;

            if (string.IsNullOrEmpty(cart))
            {
                return NotFound("Cart is empty.");
            }

            // Deserialize the cart from session
            cartItems = JsonConvert.DeserializeObject<List<Cart>>(cart);

            // Find and remove the item from the cart
            var itemToRemove = cartItems.FirstOrDefault(c => c.ProductId == cartId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
            }

            // Save updated cart back to session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            return RedirectToAction("ShowCart"); // Redirect to cart display method
        }

    }
}
