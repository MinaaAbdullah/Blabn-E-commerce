using Microsoft.AspNetCore.Mvc;
using Blabn_E_commerce.Models;
using Blabn_E_commerce.Repositories;
using System.Collections.Generic;
using Blabn_E_commerce.ViewModels;
using Newtonsoft.Json;

namespace Blabn_E_commerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Displays a list of all orders
        public IActionResult Index()
        {
            List<Order> orders = _orderRepository.GetAllOrders();
            return View(orders);
        }

        // Display details of a single order
        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Create a new order (Form display)
        public IActionResult Create(int id)
        {
            return View();
        }

        // POST: Create a new order (Form submission)
        [HttpPost]
        public IActionResult Create()
        {
            // Retrieve the current cart from the session
            var cart = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cart))
            {
                return BadRequest("Cart is empty.");
            }

            List<CartItemViewModel> cartItems = JsonConvert.DeserializeObject<List<CartItemViewModel>>(cart);

            // Check if there are any items in the cart
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Cart is empty.");
            }

            // Create a new Order
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(item => item.Price * item.Quantity),
                OrderStatus ="Will be delivred in hour",
                UserId=1
                
            };

            // Create order details for each cart item
            foreach (var item in cartItems)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            // Save the order to the database (assuming _orderRepository is defined and configured)
            _orderRepository.CreateOrder(order);
            _orderRepository.Save();

            // Clear the cart from the session after order is placed
            HttpContext.Session.Remove("Cart");

            // Redirect to a confirmation page or order summary
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }
        public IActionResult OrderConfirmation(int orderId)
        {
            // Retrieve the order from the database using the orderId
            var order = _orderRepository.GetOrderById(orderId); // Ensure this method exists and retrieves the order

            if (order == null)
            {
                return NotFound("Order not found."); // Handle the case where the order does not exist
            }

            return View(order); // Pass the order to the view
        }

    }

}

        

