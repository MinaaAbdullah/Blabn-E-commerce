namespace Blabn_E_commerce.ViewModels
{
    public class CartViewModel
    {
        public int OrderId { get; set; } // This is for the order information
        public List<CartItemViewModel> CartItems { get; set; } // List of cart items
    }
}
