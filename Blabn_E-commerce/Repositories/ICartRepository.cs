using Blabn_E_commerce.Models;
using System.Collections.Generic;

namespace Blabn_E_commerce.Repositories
{
    public interface ICartRepository
    {
        public void AddToCart(Cart cart);  // Adds an item to the cart
        public void UpdateCart(Cart cart);  // Updates the quantity or details of a cart item
        public void RemoveFromCart(int cartId);  // Removes a specific item from the cart
        public List<Cart> GetCartByUserId(int userId);  // Retrieves all cart items for a specific user
        public List<Cart> GetAllCarts();  // Gets all carts (if needed for admin purposes)
        public Cart GetCartById(int cartId);  // Retrieves a specific cart item by its ID
        public void Save();  // Saves changes to the database
        public Product GetProductById(int productId);
        //IEnumerable<object> GetCartItems(object userId);
    }
}
