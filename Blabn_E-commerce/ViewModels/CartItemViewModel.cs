namespace Blabn_E_commerce.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }  // ID of the product
        public string ProductName { get; set; }  // Name of the product
        public int Quantity { get; set; }  // Quantity of the product in the cart
        public decimal Price { get; set; }  // Price of a single unit of the product
        public decimal Total => Quantity * Price;  // Total price for the quantity of the product
    }
}
