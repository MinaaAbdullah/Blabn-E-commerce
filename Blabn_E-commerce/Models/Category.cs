using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Blabn_E_commerce.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Navigation property
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }

}
