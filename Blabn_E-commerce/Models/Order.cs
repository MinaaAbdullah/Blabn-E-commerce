using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Blabn_E_commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [ValidateNever]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        [ValidateNever]
        public string OrderStatus { get; set; }
        // Navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>(); // Initialize the collection
        }
    }

}
