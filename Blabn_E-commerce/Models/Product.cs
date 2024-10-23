using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Blabn_E_commerce.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ValidateNever]
        public string Image { get; set; }

        // Foreign key
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        // Navigation properties
        public ICollection<OrderDetail> OrderDetails { get; set; }
        [ValidateNever]
        public ICollection<Cart> Carts { get; set; }
        
    }

}
