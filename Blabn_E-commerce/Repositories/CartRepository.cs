using Blabn_E_commerce.Models;
using Blabn_E_commerce.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Blabn_E_commerce.Repositories
{
    public class CartRepository : ICartRepository
    {
        Blabn_Context _context;
        public CartRepository()
        {
            _context = new Blabn_Context();
        }

        public void AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
        }

        public void RemoveFromCart(int cartId)
        {
            var cart = _context.Carts.Find(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
        }

        // Return a list of cart items for the user
        public List<Cart> GetCartByUserId(int userId)
        {
            return _context.Carts.Where(c => c.UserId == userId).ToList();
        }

        public List<Cart> GetAllCarts()
        {
            return _context.Carts.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Cart GetCartById(int cartId)
        {
            return _context.Carts.FirstOrDefault(c => c.CartId == cartId);
        }
        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
        //public IEnumerable<CartItemViewModel> GetCartItems(int userId)
        //{
        //    var cartItems = _context.CartItems
        //        .Where(c => c.UserId == userId)
        //        .Select(c => new CartItemViewModel
        //        {
        //            ProductId = c.ProductId,
        //            ProductName = c.Product.ProductName,
        //            Quantity = c.Quantity,
        //            Price = c.Price,
        //            Total = c.Quantity * c.Price
        //        }).ToList();

        //    return cartItems;
        //}

        public IEnumerable<object> GetCartItems(object userId)
        {
            throw new NotImplementedException();
        }
    }
}
