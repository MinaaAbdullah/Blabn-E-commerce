using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Blabn_E_commerce.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        [ValidateNever]
        public int ?UserId { get; set; }
        [ValidateNever]
        public User User { get; set; }
        [ValidateNever]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

}
