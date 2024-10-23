using Blabn_E_commerce.Models;

namespace Blabn_E_commerce.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string Message { get; set; }
        public List<CartItemViewModel> CartItems { get; set; } // Cart items included
    }



}
